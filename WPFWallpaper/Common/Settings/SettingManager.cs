﻿using System;
using System.Collections.Generic;
using System.IO;
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
         * 
         * //Down here, Should be implement
         * [Music]
         * CurrentMusic
         * MusicList
         * 
         * [Quick]
         * Featurelist
         * ========================================
         */
        public static CommonSetting commonSetting = new CommonSetting();
        public static YoutubeSetting youtubeSetting = new YoutubeSetting();
        public static VideoSetting videoSetting = new VideoSetting();
        public static GifSetting gifSetting = new GifSetting();
        public static MusicSetting musicSetting = new MusicSetting();

        public static List<FeatureControl> FeatureList = new List<FeatureControl>();
        static InIManager InIManager = new InIManager();

        static readonly string Setting_Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Wallpaper\setting.ini";
        static readonly string Quick_Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Wallpaper\Quick.txt";
        public static readonly string Youtube_Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Wallpaper\1.jpg";

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

            #region MusicSetting_Load
            musicSetting.CurrentMusic = InIManager.Read_ini("Music", "CurrentMusic", Setting_Path);
            musicSetting.MusicList = InIManager.Read_ini("Music", "MusicList", Setting_Path);
            musicSetting.Volume = Convert.ToInt32(InIManager.Read_ini("Music", "Volume", Setting_Path));
            #endregion

            #region Quick
            StreamReader streamReader = new StreamReader(Quick_Path);
            while(!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                if (str != "")
                {
                    string[] quick = str.Split('|');
                    PlayLists.QuickLists.Add(new QuickModel() { Feature = Converter.ConvertStringToFeature(quick[0]), Content = quick[1] });
                }
            }
            streamReader.Close();
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

            string[] musics = musicSetting.MusicList.Split('|');
            foreach(var music in musics)
            {
                if(music!=null&&music!="")
                {
                    PlayLists.MusicLists.Add(music);
                }
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

            #region Music_Setting
            InIManager.Write_ini("Music", "CurrentMusic", musicSetting.CurrentMusic, Setting_Path);
            string musicList = "";
            foreach(var music in PlayLists.MusicLists)
            {
                musicList += music + "|";
            }
            InIManager.Write_ini("Music", "MusicList", musicList, Setting_Path);


            #endregion

            #region Quick
            StreamWriter streamWriter = new StreamWriter(Quick_Path,false);
            foreach(var item in PlayLists.QuickLists)
            {
                streamWriter.WriteLine(Converter.ConvertFeatureToString(item.Feature) + "|" + item.Content);
            }
            streamWriter.Close();
            #endregion

        }
    }
}
