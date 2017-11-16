using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        static readonly string Setting_Path = AppDomain.CurrentDomain.BaseDirectory + "setting.ini";
        public static void Load_Setting()
        {
            #region CommonSetting_Load
            commonSetting.CurrentFeature = InIManager.Read_ini("Common", "CurrentFeature", Setting_Path);
            commonSetting.CurrentContent = InIManager.Read_ini("Common", "CurrentContent", Setting_Path);
            commonSetting.CurrentMonitor = Convert.ToInt32(InIManager.Read_ini("Common", "CurrentMonitor", Setting_Path));
            commonSetting.RunAtStartup = Convert.ToBoolean(InIManager.Read_ini("Common", "RunAtStartup", Setting_Path));
            commonSetting.Volume = Convert.ToInt32(InIManager.Read_ini("Common", "Volume", Setting_Path));
            #endregion

            #region YoutubeSetting_Load
            youtubeSetting.CurrentYoutubeTitle = InIManager.Read_ini("Youtube", "CurrentTitle",Setting_Path);
            youtubeSetting.CurrentYoutubeDesc = InIManager.Read_ini("Youtube", "CurrentDesc", Setting_Path);
            #endregion

            #region VideoSetting_Load
            videoSetting.VideoList = InIManager.Read_ini("Video", "VideoList", Setting_Path);
            videoSetting.CurrentVideo = InIManager.Read_ini("Video", "CurrentVideo", Setting_Path);
            #endregion

            #region GIFSetting_Load
            gifSetting.GIFList = InIManager.Read_ini("GIF", "GIFList", Setting_Path);
            gifSetting.CurrentGIF = InIManager.Read_ini("GIF","CurrentGIF",Setting_Path);
            #endregion
        }
        public static void Save_Setting()
        {
            #region Current_Setting
            
            #endregion
        }
    }
}
