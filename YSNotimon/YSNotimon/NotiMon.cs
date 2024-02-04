using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YSNotimon;

namespace YSNotimon
{
    class NotiMon
    {
        public static bool IsRunnig = true;
        public static int LastCheckHour;

        public NotiMon()
        {
            LastCheckHour = -1;
        }

        public async Task RunAsync()
        {
            Logger.LogI("Notimon Service Start");
            await TeleGramNotiManager.NotiAsync("Notimon Service On");

            while (IsRunnig)
            {
                var now = DateTime.Now;

                if (now.Hour != LastCheckHour && now.Minute == 5)
                {
                    Logger.LogI("Do Check");

                    foreach (var data in NotiListManager.CheckList)
                    {
                        if (data.Key == "Gonak")
                        {
                            if (data.Value.IsActive == false)
                                continue;

                            var (result, message) = await Gonak.CheckPriceChange(data.Value.Url, data.Value.ProductName, data.Value.Price);

                            if (result == true)
                            {
                                await TeleGramNotiManager.NotiAsync(message);
                                ++data.Value.CurrNotiCount;
                                Logger.LogI("push telegram msg: " + message);
                            }

                            if (data.Value.CurrNotiCount >= data.Value.MaxNotiCount)
                            {
                                data.Value.IsActive = false;

                                await TeleGramNotiManager.NotiAsync(string.Format("{0} NotiService OFF", data.Value.ProductName));

                                Logger.LogI(string.Format("{0} {1} noti has been disabled", data.Key, data.Value.ProductName));
                            }
                        }
                    }

                    LastCheckHour = now.Hour;
                }

                Thread.Sleep(2_000);
            }
        }
    }
}
