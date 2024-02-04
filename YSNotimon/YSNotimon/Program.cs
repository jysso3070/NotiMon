using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using YSNotimon;

namespace YSNotimon
{
    class Program
    {
        public static string ServiceName = "YSNotimon";
        public static string FilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        static async Task Main(string[] args)
        {
            if (Environment.UserInteractive == false)
            {
                // service mode
                using (var service = new ServiceManager())
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                try
                {
                    ServiceMain();
                }
                catch(Exception e)
                {
                    Logger.LogEx(e);
                }
            }
        }

        public static void ServiceMain()
        {
            Logger.LogI("Init Service Start");

            try
            {
                // config load
                ConfigManager.LoadConfig();

                // NotiList load
                NotiListManager.LoadFromJson();

                var notiTasks = new List<Task>();
                var notiMon = new NotiMon();

                notiTasks.Add(notiMon.RunAsync());

                Task.WaitAll(notiTasks.ToArray());
            }
            catch (Exception ex)
            {
                Logger.LogEx(ex);

                Task.Run(() => TeleGramNotiManager.NotiAsync(string.Format("Notimon Exception Occurred!! \n{0}\n{1}", ex.Message, ex.StackTrace)).Wait());
            }
        }
    }
}
