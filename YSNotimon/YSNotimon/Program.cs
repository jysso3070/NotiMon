using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSNotimon;

namespace YSNotimon
{
    class Program
    {
        public static string ServiceName = "YSNotiService";

        static async Task Main(string[] args)
        {
            if (Environment.UserInteractive == false)
            {
                // running as service
                using (var service = new ServiceManager())
                {
                }
            }
            else
            {
                Run();
            }
        }

        public static void Run()
        {
            Console.WriteLine("Program Run");

            // config load
            ConfigManager cm = new ConfigManager();
            cm.LoadConfig();

            // NotiList load
            NotiListManager.LoadFromJson();

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
