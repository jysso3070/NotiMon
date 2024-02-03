using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YSNotimon
{
    class WebCrawler
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<HtmlDocument> DoCrawl(string url)
        {

            // HttpClient를 사용하여 웹 페이지의 HTML을 가져옵니다.
            string html = await httpClient.GetStringAsync(url);

            // HtmlAgilityPack을 사용하여 HTML을 파싱합니다.
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            return htmlDocument;
        }
    }
}
