using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace Parser
{
    public class GetRequest
    {
        HttpWebRequest request;
        string adress;
        public string Response { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public string Host { get; set; }
        public WebProxy Proxy { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public GetRequest (string _adress)
        {
            adress = _adress;
            Headers = new Dictionary<string, string>();
        }
        public void Run(CookieContainer cookie)
        {
            {
                request = (HttpWebRequest)WebRequest.Create(adress);
                request.Method = "GET";
                request.CookieContainer = cookie;
                request.Proxy = Proxy;
                request.Accept = Accept;
                request.Host = Host;
                request.Referer = Referer;
                request.UserAgent = UserAgent;
                foreach (var pair in Headers)
                {
                    request.Headers.Add(pair.Key, pair.Value);
                }
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        Response = new StreamReader(stream).ReadToEnd();
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}