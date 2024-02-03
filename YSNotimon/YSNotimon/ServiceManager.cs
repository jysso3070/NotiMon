using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace YSNotimon
{
    class ServiceManager : ServiceBase
    {

        public ServiceManager()
        {
            ServiceName = Program.ServiceName;
        }


        protected override void OnStart(string[] args)
        {
            // 서비스로 구동 할 때
        }

        protected override void OnStop()
        {
            // 종료 할 때
        }

    }
}
