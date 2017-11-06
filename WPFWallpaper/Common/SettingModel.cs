using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWallpaper.Common
{
    class SettingModel
    {
        string YoutubeID    { get; set; }
        string YoutubeImage { get; set; }
        string YoutubeTitle { get; set; }
        string YoutubeDesc  { get; set; }

        string VideoPath    { get; set; }

        string GifPath      { get; set; }

        string Volume       { get; set; }
        string Language     { get; set; }
        string Screen       { get; set; }

        static SettingModel Setting = new SettingModel();

        public static SettingModel GetInstance()
        {
            return Setting;
        }
    }
}
