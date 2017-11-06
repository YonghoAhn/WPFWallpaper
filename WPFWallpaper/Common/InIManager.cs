using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFWallpaper.Common
{
    class InIManager
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// InI에서 특정 섹션, 특정 키의 값을 string으로 반환합니다.
        /// </summary>
        /// <param name="section">특정 섹션입니다.</param>
        /// <param name="key">특정 키입니다.</param>
        /// <param name="path">ini 경로입니다.</param>
        /// <returns></returns>
        private static string Read_ini(string section, string key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            int ret = GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// ini에 특정 섹션, 특정 키값에 value를 저장합니다.
        /// </summary>
        /// <param name="Section">특정 섹션입니다.</param>
        /// <param name="Key">특정 키입니다.</param>
        /// <param name="Value">입력할 값입니다.</param>
        /// <param name="path">ini 경로입니다.</param>
        private static void Write_ini(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }
    }
}
