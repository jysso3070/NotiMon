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
        public static Int64 LastCheckHour;

        public NotiMon()
        {
            LastCheckHour = -1;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                // 이제 여기서 크롤링한 결과를 분석해서 보낼지 말지 검증
                // 주기도 여기서 정해보자 sleep 으로 하면 딜레이타임이 밀릴듯?
                // 특정 시간대 마다 보내도록 하는게 좋을 거 같음.
                // ex 매 시간 5분, 35 분 or 10분마다 한번씩?

                var now = DateTime.Now;

                if(now.Hour != LastCheckHour && now.Minute == 5)
                {
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
                                Logger.Log("[PUSH] telegram msg: " + message);
                            }

                            if (data.Value.CurrNotiCount >= data.Value.MaxNotiCount)
                            {
                                data.Value.IsActive = false;

                                await TeleGramNotiManager.NotiAsync(string.Format("{0} NotiService OFF", data.Value.ProductName));

                                Logger.Log(string.Format("[INFO] {0} {1} noti has been disabled", data.Key, data.Value.ProductName));
                            }
                        }
                    }

                    LastCheckHour = now.Hour;
                }

                Thread.Sleep(20_000);
            }
        }
    }
}
