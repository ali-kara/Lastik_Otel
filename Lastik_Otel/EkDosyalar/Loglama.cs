using System;
using System.IO;
using System.Reflection;

namespace Lastik_Otel
{
    public static class Loglama
    {

        static string fileLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log.txt";
        static string fileLocKritikHata = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\LogKritikHata.txt";

        public static void dosyaKontrolu()
        {
            if (!File.Exists(fileLoc))
            {
                using (FileStream fs = File.Create(fileLoc))
                {

                }
            }
            if (!File.Exists(fileLocKritikHata))
            {
                using (FileStream fs = File.Create(fileLocKritikHata))
                {

                }
            }
        }

        public static void Write(string Log)
        {
            dosyaKontrolu();
            using (StreamWriter sw = File.AppendText(fileLoc))
            {
                sw.WriteLine(DateTime.Now + "                           " + Log);
            }
        } 

        public static void WriteError(string Log)
        {
            dosyaKontrolu();

            using (StreamWriter sw = File.AppendText(fileLocKritikHata))
            {
                sw.WriteLine(DateTime.Now + "                           " + Log);
            }
        }
    }
}