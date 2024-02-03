using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YSNotimon
{
    public class NotiListManager
    {
        public static Dictionary<string, ProductInfo> CheckList = new Dictionary<string, ProductInfo>();

        public class ProductInfo
        {
            public string SiteName { get; set; }
            public string ProductName { get; set; }
            public int Price { get; set; }
            public string Url { get; set; }
            public int MaxNotiCount { get; set; }
            public int CurrNotiCount { get; set; }
            public bool IsActive { get; set; }

        }

        public static void LoadFromJson()
        {
            string jsonData = File.ReadAllText("NotiList.json");

            JObject jsonObject = JObject.Parse(jsonData);
            JToken jToken = jsonObject["List"];

            foreach (var data in jToken)
            {
                ProductInfo info = new ProductInfo();

                info.SiteName = data.Value<string>("SiteName");
                info.ProductName = data.Value<string>("ProductName");
                info.Url = data.Value<string>("Url");
                info.Price = data.Value<int>("Price");
                info.MaxNotiCount = data.Value<int>("MaxNoti");
                info.IsActive = data.Value<int>("Active") == 0 ? false : true;
                info.CurrNotiCount = 0;
                


                CheckList[info.SiteName] = info;
            }
        }
    }
}
