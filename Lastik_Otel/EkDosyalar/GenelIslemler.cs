using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lastik_Otel.EkDosyalar
{
    public static class GenelIslemler
    {
        public static void killWordInstance()
        {
            Process[] words = Process.GetProcessesByName("WINWORD");

            foreach (Process process in words)
            {
                process.Kill();
            }
        }
    }
}
