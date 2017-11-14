using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFWallpaper.Common.Settings.UserSetting
{
    public class YoutubeSetting
    {
        public string CurrentYoutubeTitle { get; set; }
        public string CurrentYoutubeDesc { get; set; }
        public string CurrentYoutubeImage = Application.Current.StartupUri.AbsolutePath.ToString();
    }
}
