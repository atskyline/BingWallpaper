using System;
using System.IO;
using System.Net;
using System.Xml;

namespace BingWallpaper
{
    internal class BingApi
    {
        public static readonly String[] LocationOptions = { "en-US", "zh-CN", "ja-JP", "en-AU", "de-DE", "en-NZ", "en-CA" };
        private const String BingUrl = "http://www.bing.com";
        private const String ApiUrlBase = BingUrl + "/HPImageArchive.aspx";

        //对应API中的idx参数，表示时间，-1为明天，1为昨天，2为后天，依次类推，已知可选项-1 ~ 18，默认为0
        public Int32 Time = 0;
        //对应API中的mkt参数，表示地区，可选项见LocationOptions，如果为null或空字符串则随机从LocationOptions中选择一个，默认为null
        public String Location = null;

        private String _apiUrl;
        private String _defaultImageUrl;
        private String _imageUrlBase;

        public void Init()
        {
            Refresh();
        }
        public void Refresh()
        {
            InitApiUrl();
            Console.WriteLine("开始请求BingApi:"+_apiUrl);
            WebRequest req = WebRequest.Create(_apiUrl);
            String response;
            using (WebResponse res = req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            Console.WriteLine("请求BingApi成功");
            ParseResponse(response);
        }

        private void ParseResponse(String res)
        {
            Console.WriteLine("开始解析BingApi返回值...");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            
            var urlNode = doc.SelectSingleNode("//url");
            if (urlNode == null)
            {
                throw new Exception("解析//url失败：" + res);
            }
            _defaultImageUrl = BingUrl + urlNode.InnerText;

            var urlBaseNode = doc.SelectSingleNode("//urlBase");
            if (urlBaseNode == null)
            {
                throw new Exception("解析//urlBase失败：" + res);
            }
            _imageUrlBase = BingUrl + urlBaseNode.InnerText;
            Console.WriteLine("解析BingApi返回值成功");
        }

        private void InitApiUrl()
        {
            if (String.IsNullOrEmpty(Location))
            {
                int randIndex = new Random().Next(0, LocationOptions.Length);
                Location = LocationOptions[randIndex];
            }
            _apiUrl = ApiUrlBase + "?/format=xml&n=1&idx=" + Time + "&mkt=" + Location;
        }


        /// <summary>
        /// 获取图片的地址，如果resolution为null或空字符串将返回默认url
        /// 指定resolution后的地址可能不存在
        /// resolution已知可选项有640x480,800x600,1024x768,1280x720,1920x1080,800x480,1366x768,1920x1200,1280x768
        /// </summary>
        public String GetImageUrl(String resolution = null)
        {
            if (String.IsNullOrEmpty(resolution))
            {
                return _defaultImageUrl;
            }
            return _imageUrlBase + "_" + resolution + ".jpg";
        }
    }
}
