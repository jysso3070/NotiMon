using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSNotimon
{
    class Gonak
    {
        public static async Task<(bool, string)> CheckPriceChange(string url, string productName, int CheckPrice)
        {
            var result = false;
            var resultString = "";
            var htmlDocument = await WebCrawler.DoCrawl(url);

            string id = "shopProductPrice";
            string className = "productDiscountPriceSpan";
            string xpath = $"//div[@id='{id}']//span[@class='{className}']";

            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes(xpath);

            if (nodes != null && nodes.Count > 0)
            {
                foreach (HtmlNode node in nodes)
                {
                    var curPrice = Convert.ToInt32(new string(node.InnerText.Where(char.IsDigit).ToArray()));

                    if (CheckPrice != curPrice)
                    {
                        result = true;
                        resultString += $"\"{productName}\" Price Change \n {CheckPrice} -> {curPrice} \n" + $"link: {url} \n";
                    }
                }
            }
            else
            {
                Console.WriteLine("No matching nodes found.");
            }

            return (result, resultString);
        }
    }
}
