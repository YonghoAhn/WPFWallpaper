using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWallpaper.Common.Settings.UserSetting;

namespace WPFWallpaper.Common.Settings
{
    public static class SettingManager
    {
        /*
         * Setting.ini
         * 
         * [Common]
         * CurrentFeature=Youtube
         * CurrentMonitor=1
         * 
         * 
         */
        static CommonSetting commonSetting = new CommonSetting();
        static YoutubeSetting youtubeSetting = new YoutubeSetting();
        static VideoSetting videoSetting = new VideoSetting();
        static GifSetting gifSetting = new GifSetting();

        static InIManager InIManager = new InIManager();
        public static void Load_Setting()
        {

        }
        public static void Save_Setting()
        {
            
        }
    }
}
