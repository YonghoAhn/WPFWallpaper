using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFWallpaper.Common;
using WPFWallpaper.Common.Settings;
using WPFWallpaper.Forms;
using WPFWallpaper.Models;
using WPFWallpaper.Pages;
using WPFWallpaper.Windows;
using setting = WPFWallpaper.Common.Settings.SettingManager;
namespace WPFWallpaper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;
         


        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        static YoutubePage youtubePage;
        static VideoPage videoPage;
        static GifPage gifPage;
        static SettingPage settingPage;
        static string featureContent;
        public static string FeatureContent { get { return featureContent; } set{ featureContent = value; OnStaticPropertyChanged("FeatureContent"); } }



        public MainWindow()
        {
            InitializeComponent();
            setting.Load_Setting();
            youtubePage = new YoutubePage();
            videoPage = new VideoPage();
            gifPage = new GifPage();
            settingPage = new SettingPage();
            InstantList.ItemsSource = PlayLists.QuickLists;
            //Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + "setting.ini");
        }

        private void MainWindow1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// ToggleButton(In XAML, RadioButton) Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Name)
            {
                case "YoutubeToggle":
                    SetPage("YouTube");
                    break;
                case "VideoToggle":
                    SetPage("Video");
                    break;
                case "GifToggle":
                    SetPage("GIF");
                    break;
                case "SettingToggle":
                    SetPage("Setting");
                    break;
            }
        }
        private void SetPage(string feature)
        {
            switch (feature)
            {
                case "YouTube":
                    MainFrame.Navigate(youtubePage);
                    setting.commonSetting.CurrentFeature = Feature.Youtube;
                    CurrentFeatureLabel.Content = "Youtube";
                    break;
                case "Video":
                    MainFrame.Navigate(videoPage);
                    setting.commonSetting.CurrentFeature = Feature.Video;
                    CurrentFeatureLabel.Content = "Video";
                    break;
                case "GIF":
                    MainFrame.Navigate(gifPage);
                    setting.commonSetting.CurrentFeature = Feature.GIF;
                    CurrentFeatureLabel.Content = "Gif";
                    break;
                case "Setting":
                    MainFrame.Navigate(settingPage);

                    setting.commonSetting.CurrentFeature = Feature.Empty;
                    CurrentFeatureLabel.Content = "Setting";
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {

            ScreenUtility.Initialize();
            MonitorCombo.ItemsSource = ScreenUtility.ScreenCollection;
            setting.FeatureList.Add(new FeatureControl() { form = null, window=null, Content = "", feature = Feature.Empty, monitor = 0 });
            //Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "WPFWallpaper.exe", 11001);
            switch (setting.commonSetting.CurrentFeature)
            {
                case Feature.Youtube:
                    YoutubeToggle.IsChecked = true;
                    SetPage("YouTube");
                    break;
                case Feature.Video:
                    VideoToggle.IsChecked = true;
                    SetPage("Video");
                    break;
                case Feature.GIF:
                    GifToggle.IsChecked = true;
                    SetPage("GIF");
                    break;
            }
        }

        private void MonitorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setting.commonSetting.CurrentMonitor = MonitorCombo.SelectedIndex;
            if (setting.commonSetting.CurrentMonitor == -1)
                setting.commonSetting.CurrentMonitor = 0;
            if (setting.FeatureList.Count <= SettingManager.commonSetting.CurrentMonitor)
            {
                setting.FeatureList.Add(new FeatureControl() { form = null, window=null });
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControl featureControl = setting.FeatureList[setting.commonSetting.CurrentMonitor];
            Feature feature = setting.commonSetting.CurrentFeature;
            StopWallpaper();
            if (!CheckEmpty())
            {
                string currentContent = "";
                switch (feature)
                {
                    case Feature.Youtube:
                        currentContent = setting.youtubeSetting.CurrentYoutubeContent;
                        YoutubeWindow youtube = new YoutubeWindow(currentContent, setting.commonSetting.CurrentMonitor);
                        youtube.Show();
                        featureControl.window = youtube;
                        break;
                    case Feature.Video:
                        currentContent = setting.videoSetting.CurrentVideo;
                        VideoForm video = new VideoForm(currentContent, setting.commonSetting.CurrentMonitor);
                        video.Show();
                        featureControl.form = video;
                        break;
                    case Feature.GIF:
                        currentContent = setting.gifSetting.CurrentGIF;
                        GifForm gif = new GifForm(currentContent, setting.commonSetting.CurrentMonitor);
                        gif.Show();
                        featureControl.form = gif;  
                        break;
                }
                featureControl.Content = currentContent;
                featureControl.feature = feature;

            }
        }

        private bool CheckEmpty()
        {
            if (setting.commonSetting.CurrentContent != null)
                return false;
            else
                return true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopWallpaper();
        }

        private void StopAllButton_Click(object sender, RoutedEventArgs e)
        {
            StopAllWallpaper();
        }

        private void StopWallpaper()
        {
            FeatureControl featureControl = setting.FeatureList[setting.commonSetting.CurrentMonitor];
            if (featureControl.form != null)
                featureControl.form.Close();
            if (featureControl.window != null)
                featureControl.window.Close();
        }

        private void StopAllWallpaper()
        {
            foreach (FeatureControl featureControl in setting.FeatureList)
            {
                if (featureControl.form != null)
                    featureControl.form.Close();
                if (featureControl.window != null)
                    featureControl.window.Close();
            }
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopAllWallpaper();
            setting.Save_Setting();
        }

        private void AddQuickButton_Click(object sender, RoutedEventArgs e)
        {
            PlayLists.QuickLists.Add(new QuickModel() { Content = setting.commonSetting.CurrentContent, Feature = setting.commonSetting.CurrentFeature });
        }

        private void RemoveQuickButton_Click(object sender, RoutedEventArgs e)
        {
            if(InstantList.SelectedItem!=null)
            {
                PlayLists.QuickLists.Remove(InstantList.SelectedItem as QuickModel);
            }
        }

        private void InstantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(InstantList.SelectedItem != null)
            {
                setting.commonSetting.CurrentContent = (InstantList.SelectedItem as QuickModel).Content;
                setting.commonSetting.CurrentFeature = (InstantList.SelectedItem as QuickModel).Feature;

            }
        }
    }
}
