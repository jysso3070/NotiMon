using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YSNotimon
{
    class HttpManager
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<string> PostAsync(string url, string requestBody)
        {
            try
            {
                // POST 요청을 보낼 때는 StringContent 를 사용하여 요청 본문을 설정합니다.
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // HttpClient를 사용하여 POST 요청을 보냅니다.
                HttpResponseMessage response = await HttpClient.PostAsync(url, content);

                // 응답이 성공적이면 응답 본문을 문자열로 반환합니다.
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // 실패한 경우 응답 상태 코드를 예외로 처리하거나 다른 처리를 수행할 수 있습니다.
                    throw new HttpRequestException($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // 예외가 발생한 경우 처리합니다.
                Logger.LogE($"HTTP request failed: {ex.Message}");
                return null;
            }
        }
    }
}
