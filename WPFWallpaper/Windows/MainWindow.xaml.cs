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
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + "setting.ini");
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
                    MainFrame.Navigate(youtubePage);
                    setting.commonSetting.CurrentFeature = Feature.Youtube;
                    CurrentFeatureLabel.Content = "Youtube";
                    break;
                case "VideoToggle":
                    MainFrame.Navigate(videoPage);
                    setting.commonSetting.CurrentFeature = Feature.Video;
                    CurrentFeatureLabel.Content = "Video";
                    break;
                case "GifToggle":
                    MainFrame.Navigate(gifPage);

                    setting.commonSetting.CurrentFeature = Feature.GIF;
                    CurrentFeatureLabel.Content = "Gif";
                    break;
                case "SettingToggle":
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
                switch (feature)
                {
                    case Feature.Youtube:
                        YoutubeWindow youtube = new YoutubeWindow(setting.commonSetting.CurrentContent, setting.commonSetting.CurrentMonitor);
                        youtube.Show();
                        featureControl.window = youtube;
                        break;
                    case Feature.Video:
                        VideoForm video = new VideoForm(setting.commonSetting.CurrentContent, setting.commonSetting.CurrentMonitor);
                        video.Show();
                        featureControl.form = video;
                        break;
                    case Feature.GIF:
                        GifForm gif = new GifForm(setting.commonSetting.CurrentContent, setting.commonSetting.CurrentMonitor);
                        gif.Show();
                        featureControl.form = gif;  
                        break;
                }
                featureControl.Content = setting.commonSetting.CurrentContent;
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
    }
}
