using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitypackageCrypter_Linux.ThreadHandler;


namespace UnitypackageCrypter_Linux
{
    public class EntryPoint
    {
        public static void StartIt(bool saveStart = false)
        {
            if (saveStart)
            {
                ThreadHandler.ThreadHandler.StartHandling();
            }
        }
    }
}