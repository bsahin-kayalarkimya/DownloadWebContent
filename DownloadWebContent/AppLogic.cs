using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DownloadWebContent
{
    internal class AppLogic
    {
        private string fromUrl { get; set; }
        private string toPath { get; set; }

        public void DoJob()
        {
            GetParams();
            DownloadContent();
        }

        private void GetParams()
        {
            try
            {
                fromUrl = ConfigurationManager.AppSettings["FromUrl"];
                toPath = ConfigurationManager.AppSettings["ToPath"];
            }
            catch (Exception ex)
            {
                fromUrl = String.Empty;
                toPath = String.Empty;

                var sMethodBase = MethodBase.GetCurrentMethod();
                var sClassMethodName = $"{sMethodBase.ReflectedType.Name}.{sMethodBase.Name}";

                AppLog.SaveLog(AppLog.eLogType.ERR, sClassMethodName + " - " + ex.Message);
            }
        }

        private void DownloadContent()
        {
            if (String.IsNullOrEmpty(fromUrl) || String.IsNullOrEmpty(toPath))
                return;

            var sMethodBase = MethodBase.GetCurrentMethod();
            var sClassMethodName = $"{sMethodBase.ReflectedType.Name}.{sMethodBase.Name}";

            try
            {
                if (File.Exists(toPath))
                {
                    File.Delete(toPath);
                    AppLog.SaveLog(AppLog.eLogType.INF, "Deleted - " + toPath);
                }

                WebClient sWebClient = new WebClient();
                sWebClient.DownloadFile(fromUrl, toPath);

                AppLog.SaveLog(AppLog.eLogType.INF, "Success - " + fromUrl + " > " + toPath);
            }
            catch (Exception ex)
            {
                AppLog.SaveLog(AppLog.eLogType.ERR, sClassMethodName + " - " + ex.Message);
            }
        }
    }
}
