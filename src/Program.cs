using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BingWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private readonly BingApi _bingApi = new BingApi();
        //Config Begin
        private Int32 _time;
        private String _location;
        private String _resolution;
        private String _savePath;
        //Config End

        private void Run()
        {
            ReadConfig();
            _bingApi.Time = _time;
            _bingApi.Location = _location;
            try
            {
                _bingApi.Init();
                String localImagePath = DownloadImage();
                Console.WriteLine("开始设置壁纸：" + localImagePath);
                Wallpaper.Set(localImagePath, Wallpaper.Style.Stretched);
                Console.WriteLine("设置壁纸成功");
            }
            catch (WebException e)
            {
                Console.WriteLine("网络连接失败，请连接网络后重试。");
            }
            catch (Exception e)
            {
                Console.WriteLine("程序运行发生异常：" + e.Message);
            }
        }

        private String DownloadImage()
        {
            String imgUrl = _bingApi.GetImageUrl(_resolution);
            String localPath = Path.Combine(_savePath, Path.GetFileName(imgUrl));
            Console.WriteLine("开始下载" + imgUrl + "到" + localPath);
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(imgUrl, localPath);
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.WriteLine("下载指定分辨率" + _resolution + "图片失败，开始尝试下载默认分辨率图片...");
                        imgUrl = _bingApi.GetImageUrl();
                        localPath = Path.Combine(_savePath, Path.GetFileName(imgUrl));
                        Console.WriteLine("开始下载" + imgUrl + "到" + localPath);
                        client.DownloadFile(imgUrl, localPath);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            Console.WriteLine("下载图片成功");
            return localPath;
        }

        private void ReadConfig()
        {
            Console.WriteLine("开始读取配置文件");
            const String section = "BingWallpaper";
            IniFile config = new IniFile(@".\config.ini");
            if (!Int32.TryParse(config.Read(section, "Time"), out _time))
            {
                _time = 0;
            }
            _location = config.Read(section, "Location");
            _resolution = config.Read(section, "Resolution");
            if (String.IsNullOrEmpty(_resolution))
            {
                Rectangle screen = Screen.PrimaryScreen.Bounds;
                _resolution = screen.Width + "x" + screen.Height;
            }
            _savePath = config.Read(section, "SavePath");
            if (String.IsNullOrEmpty(_savePath))
            {
                _savePath = Path.GetTempPath();
            }
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
            Console.WriteLine("读取配置成功");
        }
    }
}
