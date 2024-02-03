using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YSNotimon;

namespace YSNotimon
{
    class TeleGramNotiManager
    {
        public static async Task<string> NotiAsync(string message)
        {
            string url = $"{ConfigManager.TelegramUrl}/{ConfigManager.TelegramBotToken}/sendMessage?";

            var data = new
            {
                chat_id = ConfigManager.TelegramChatId,
                text = message,
            };

            string jsonString = JsonSerializer.Serialize(data);

            return await HttpManager.PostAsync(url, jsonString);
        }
    }
}
