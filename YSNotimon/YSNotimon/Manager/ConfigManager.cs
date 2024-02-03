using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace YSNotimon
{
    public class ConfigManager
    {
        public static string TelegramUrl { get; set; }
        public static string TelegramBotToken { get; set; }
        public static string TelegramChatId { get; set; }
        public static int NotiCount { get; set; }

        //public class Config
        //{
        //    public TelegramConfig Telegram { get; set; }
        //}

        //public class TelegramConfig
        //{
        //    public string Url { get; set; }
        //    public string BotToken { get; set; }
        //    public string ChatId { get; set; }
        //}

        public void LoadConfig()
        {
            string yamlContent = "";

            try
            {
                yamlContent = File.ReadAllText("Config.yml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //var deserializer = new DeserializerBuilder()
            //    .WithNamingConvention(CamelCaseNamingConvention.Instance)
            //    .Build();
            //var config = deserializer.Deserialize<Config>(yamlContent);

            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(new StringReader(yamlContent));

            TelegramUrl = result["telegram"]["url"];
            TelegramBotToken = result["telegram"]["bottoken"];
            TelegramChatId = result["telegram"]["chatid"];
            TelegramChatId = result["telegram"]["chatid"];

            NotiCount = Convert.ToInt32(result["constant"]["noticount"]);
          
        }
    }
}
