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
        /* Setting.ini
         * ========================================
         * [Common]
         * CurrentFeature=Youtube
         * CurrentMonitor=1
         * CurrentContent=Path
         * RunAtStartup=false
         * Volume=100
         * 
         * [Youtube]
         * CurrentTitle=Title
         * CurrentDesc=Description
         * CurrentImage=ImagePath
         * 
         * [Video]
         * VideoList=asdf.mp4|saf.mp4|...
         * CurrentVideo=asfdsadf.mp4
         * 
         * [GIF]
         * GIFList=asdf.gif|asfd.gif|...
         * CurrentGIF=Path
         * ========================================
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
