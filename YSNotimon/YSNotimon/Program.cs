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

        static async Task Main(string[] args)
        {
            if (Environment.UserInteractive == false)
            {
                // running as service
                using (var service = new ServiceManager())
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                ServiceMain();
            }
        }

        public static void ServiceMain()
        {
            Logger.LogI("ServiceMain");

            try
            {
                // config load
                ConfigManager.LoadConfig();

                // NotiList load
                NotiListManager.LoadFromJson();
            }
            catch (Exception ex)
            {
                Logger.LogEx(ex);
            }

            // Task
            var notiTasks = new List<Task>();
            var notiMon = new NotiMon();

            notiTasks.Add(notiMon.RunAsync());
            try
            {
                // 여기서 메인스레드 블락킹
                Task.WaitAll(notiTasks.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
