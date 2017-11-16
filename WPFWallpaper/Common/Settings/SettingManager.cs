using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFWallpaper.Common.Settings.UserSetting;
using WPFWallpaper.Models;

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
        public static CommonSetting commonSetting = new CommonSetting();
        public static YoutubeSetting youtubeSetting = new YoutubeSetting();
        public static VideoSetting videoSetting = new VideoSetting();
        public static GifSetting gifSetting = new GifSetting();
        public static List<FeatureControl> FeatureList = new List<FeatureControl>();
        static InIManager InIManager = new InIManager();

        static readonly string Setting_Path = AppDomain.CurrentDomain.BaseDirectory + "setting.ini";
        public static readonly string Youtube_Path = AppDomain.CurrentDomain.BaseDirectory + "1.jpg";

        public static void Load_Setting()
        {
            #region CommonSetting_Load
            commonSetting.CurrentFeature = Converter.ConvertStringToFeature(InIManager.Read_ini("Common", "CurrentFeature", Setting_Path));
            commonSetting.CurrentContent = InIManager.Read_ini("Common", "CurrentContent", Setting_Path);
            commonSetting.CurrentMonitor = Convert.ToInt32(InIManager.Read_ini("Common", "CurrentMonitor", Setting_Path));
            commonSetting.RunAtStartup = Convert.ToBoolean(InIManager.Read_ini("Common", "RunAtStartup", Setting_Path));
            commonSetting.Volume = Convert.ToInt32(InIManager.Read_ini("Common", "Volume", Setting_Path));
            #endregion

            #region YoutubeSetting_Load
            youtubeSetting.CurrentYoutubeTitle = InIManager.Read_ini("Youtube", "CurrentTitle", Setting_Path);
            youtubeSetting.CurrentYoutubeDesc = InIManager.Read_ini("Youtube", "CurrentDesc", Setting_Path);
            youtubeSetting.CurrentYoutubeContent = InIManager.Read_ini("Youtube", "CurrentContent", Setting_Path);
            #endregion

            #region VideoSetting_Load
            videoSetting.VideoList = InIManager.Read_ini("Video", "VideoList", Setting_Path);
            videoSetting.CurrentVideo = InIManager.Read_ini("Video", "CurrentVideo", Setting_Path);
            #endregion

            #region GIFSetting_Load
            gifSetting.GIFList = InIManager.Read_ini("GIF", "GIFList", Setting_Path);
            gifSetting.CurrentGIF = InIManager.Read_ini("GIF", "CurrentGIF", Setting_Path);
            #endregion

            string[] videos = videoSetting.VideoList.Split('|');
            foreach (var video in videos)
            {
                if(video!=""&&video!=null)
                PlayLists.VideoLists.Add(video);
            }

            string[] gifs = gifSetting.GIFList.Split('|');
            foreach (var gif in gifs)
            {
                if(gif!=null&&gif!="")
                PlayLists.GifLists.Add(gif);
            }
        }
        public static void Save_Setting()
        {
            #region Current_Setting
            InIManager.Write_ini("Common", "CurrentFeature", Converter.ConvertFeatureToString(commonSetting.CurrentFeature), Setting_Path);
            InIManager.Write_ini("Common", "CurrentContent", commonSetting.CurrentContent, Setting_Path);
            InIManager.Write_ini("Common", "CurrentMonitor", commonSetting.CurrentMonitor.ToString(), Setting_Path);
            InIManager.Write_ini("Common", "RunAtStartup", commonSetting.RunAtStartup.ToString(), Setting_Path);
            InIManager.Write_ini("Common", "Volume", commonSetting.Volume.ToString(), Setting_Path);
            #endregion

            #region YoutubeSetting_Load
            InIManager.Write_ini("Youtube", "CurrentTitle", youtubeSetting.CurrentYoutubeTitle, Setting_Path);
            InIManager.Write_ini("Youtube", "CurrentDesc", youtubeSetting.CurrentYoutubeDesc, Setting_Path);
            InIManager.Write_ini("Youtube", "CurrentContent", youtubeSetting.CurrentYoutubeContent, Setting_Path);
            #endregion

            #region VideoSetting_Load
            string list = "";
            foreach (var video in PlayLists.VideoLists)
                list += video + "|";
            InIManager.Write_ini("Video", "VideoList",list, Setting_Path);
            InIManager.Write_ini("Video", "CurrentVideo", videoSetting.CurrentVideo, Setting_Path);

            #endregion

            #region GIFSetting_Load
            string giflist = "";
            foreach (var gif in PlayLists.GifLists)
                giflist += gif + "|";
            InIManager.Write_ini("GIF", "GIFList", giflist, Setting_Path);
            InIManager.Write_ini("GIF", "CurrentGIF", gifSetting.CurrentGIF, Setting_Path);
            #endregion
        }
    }
}
