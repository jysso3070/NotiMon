﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YSNotimon
{
    class ServiceManager : ServiceBase
    {
        Thread ServiceThread;
        public ServiceManager()
        {
            ServiceName = Program.ServiceName;
        }


        protected override void OnStart(string[] args)
        {
            try
            {
                NotiMon.IsRunnig = true;
                ServiceThread = new Thread(Program.ServiceMain);
                ServiceThread.Start();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            
        }

        protected override void OnStop()
        {
            // 종료
            NotiMon.IsRunnig = false;

            Thread.Sleep(3000);

            Task.Run(() => TeleGramNotiManager.NotiAsync("Notimon Service OFF")).Wait();
        }

    }
}
