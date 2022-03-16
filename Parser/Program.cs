using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var code = "205104160";
            var proxy = new WebProxy();

            var postRequest = new PostRequest("https://baucenter.ru/");
            var cookieContainer = new CookieContainer();
            postRequest.Data = $"ajax_call=y&INPUT_ID=title-search-input&q={code}&l=2";
            postRequest.Accept = "*/*";
            postRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.70";
            postRequest.ContentType = "application/x-www-form-urlencoded";
            postRequest.Host = "baucenter.ru";
            postRequest.Proxy = proxy;
            postRequest.Headers.Add("Bx-ajax", "true");
            postRequest.Headers.Add("Origin", "https://baucenter.ru/");
            postRequest.Headers.Add("sec-ch-ua", "\"Opera GX\";v=\"83\",\"Chromium\";v=\"97\", \"; Not A Brand\";v=\"99\"");
            postRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            postRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            postRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            postRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            postRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            postRequest.Referer = "https://baucenter.ru/";
            postRequest.Run(cookieContainer);
            var strStart = postRequest.Response.IndexOf("search-result-group search-result-product");
            strStart = postRequest.Response.IndexOf("<a href=",strStart)+9;
            var strEnd = postRequest.Response.IndexOf("\"", strStart);
            var getPath = postRequest.Response.Substring(strStart,strEnd-strStart);
          

            var getRequest=new GetRequest($"https://baucenter.ru{getPath}");

            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.109 Safari/537.36 OPR/84.0.4316.36";
            getRequest.Referer = "https://baucenter.ru/";
            getRequest.Host = "baucenter.ru";

            getRequest.Headers.Add("sec-ch-ua", "\"Opera GX\";v=\"83\",\"Chromium\";v=\"97\", \"; Not A Brand\";v=\"99\"");
            getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            getRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            getRequest.Headers.Add("Sec-Fetch-User", "?1");
            getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer);

            var Card = new Card();
            Card.Parse(getRequest.Response);

            Console.WriteLine($">>>{Card.Title}<<<");
            Console.WriteLine($"Его цена = {Card.Price} RUB");
        }
    }
}
