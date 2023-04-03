using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebContent
{
    internal class AppLog
    {
        public enum eLogType
        {
            ERR,
            INF
        }

        public static void SaveLog(eLogType pLogType, string pStrValue)
        {
            try
            {
                string sLogContent = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " >> " + pLogType.ToString() + " >> " + pStrValue;

                string sStrLogDirPath = (Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf('\\')));
                sStrLogDirPath += @"\Logs";

                if (!Directory.Exists(sStrLogDirPath))
                    Directory.CreateDirectory(sStrLogDirPath);

                StreamWriter writer = File.AppendText(sStrLogDirPath + @"\Log-" + DateTime.Now.ToString("yyyy-MM") + ".txt");
                writer.WriteLine(sLogContent);
                writer.Close();

                Console.WriteLine(sLogContent);
            }
            catch
            { }
        }
    }
}
