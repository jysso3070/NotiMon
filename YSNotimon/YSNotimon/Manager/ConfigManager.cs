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

        public static void LoadConfig()
        {
            string yamlPath = Path.Combine(Program.FilePath, "Config.yml");

            //yamlContent = File.ReadAllText("Config.yml");
            string yamlContent = File.ReadAllText(yamlPath);

            //var deserializer = new DeserializerBuilder()
            //    .WithNamingConvention(CamelCaseNamingConvention.Instance)
            //    .Build();
            //var config = deserializer.Deserialize<Config>(yamlContent);

            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(new StringReader(yamlContent));

            TelegramUrl = result["telegram"]["url"];
            TelegramBotToken = result["telegram"]["bottoken"];
            TelegramChatId = result["telegram"]["chatid"];

            Logger.LogI("LoadConfig success");
        }
    }
}
