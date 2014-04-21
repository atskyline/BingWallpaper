using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BingWallpaper
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class IniFile
    {
        //传递给GetPrivateProfileString的size参数，读取时最多只能读取到ValueMaxLenght个字符
        public Int32 ValueMaxLenght = 2048;

        private readonly String _path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, 
            string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string filePath)
        {
            _path = filePath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public string Read(string section, string key, string def = "")
        {
            StringBuilder sb = new StringBuilder(ValueMaxLenght);
            GetPrivateProfileString(section, key, def, sb, ValueMaxLenght, _path);
            return sb.ToString();
        }
    }
}
