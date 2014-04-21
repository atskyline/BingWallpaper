using System;
using System.IO;
using System.Net;
using System.Xml.XPath;

namespace TestBaseAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            IniFile config = new IniFile(@".\config.ini");
            config.Write("Test", "testKey", "testValue");
            Console.WriteLine(config.Read("Test","testKey"));
            Console.ReadLine();
        }

        private static void TestDownloadFile()
        {
            String localPath = Path.Combine(Path.GetTempPath(), "wallpaper.jpg");
            String url = @"http://www.bing.com/az/hprichbg/rb/ZakimBunkerHillBridge_EN-US12770652204_1366x768.jpg";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, localPath);
                }
                catch (WebException e)
                {
                    Console.WriteLine("网络连接失败" + e.Message);
                }
            }
        }

        private static void TestHttpRequest()
        {
            try
            {
                string url = "http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US";
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                String content = sr.ReadToEnd();
                Console.WriteLine(content);
            }
            catch (WebException e)
            {
                Console.WriteLine("网络连接失败");
            }
        }

        private static void TestSetWallpaper()
        {
            Wallpaper.Set(@".\wallpapaer1.jpg", Wallpaper.Style.Stretched);
        }

        private static void TestXPath()
        {
            XPathDocument doc = new XPathDocument(@".\response.xml");
            XPathNavigator nav = doc.CreateNavigator();

            var urlNode = nav.SelectSingleNode("//url");
            if (urlNode != null)
            {
                String url = urlNode.ToString();
                Console.WriteLine("url = " + url);
            }

            var urlBaseNode = nav.SelectSingleNode("//urlBase");
            if (urlBaseNode != null)
            {
                String urlBase = urlBaseNode.ToString();
                Console.WriteLine("urlBase = " + urlBase);
            }
        }

    }
}
