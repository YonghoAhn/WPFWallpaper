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

        static YoutubePage youtubePage = new YoutubePage();
        static VideoPage videoPage = new VideoPage();
        static GifPage gifPage = new GifPage();
        static SettingPage settingPage = new SettingPage();
        static string featureContent;
        public static string FeatureContent { get { return featureContent; } set{ featureContent = value; OnStaticPropertyChanged("FeatureContent"); } }



        public MainWindow()
        {
            InitializeComponent();
            SettingManager.Load_Setting();
            
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
                    CurrentFeature.feature = Feature.Youtube;
                    CurrentFeatureLabel.Content = "Youtube";
                    break;
                case "VideoToggle":
                    MainFrame.Navigate(videoPage);
                    CurrentFeature.feature = Feature.Video;
                    CurrentFeatureLabel.Content = "Video";
                    break;
                case "GifToggle":
                    MainFrame.Navigate(gifPage);

                    CurrentFeature.feature = Feature.GIF;
                    CurrentFeatureLabel.Content = "Gif";
                    break;
                case "SettingToggle":
                    MainFrame.Navigate(settingPage);

                    CurrentFeature.feature = Feature.Empty;
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
            CurrentFeature.featurelist.Add(new FeatureControl() { form = null, window = null, Content = "", feature = Feature.Empty, monitor = 0 });
            //Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "WPFWallpaper.exe", 11001);

        }

        private void MonitorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentFeature.SelectedMonitor = MonitorCombo.SelectedIndex;
            if (CurrentFeature.SelectedMonitor == -1)
                CurrentFeature.SelectedMonitor = 0;
            if (CurrentFeature.featurelist.Count <= CurrentFeature.SelectedMonitor)
            {
                CurrentFeature.featurelist.Add(new FeatureControl() { window = null });
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControl featureControl = CurrentFeature.featurelist[CurrentFeature.SelectedMonitor];
            Feature feature = CurrentFeature.feature;
            StopWallpaper();
            if (!CheckEmpty())
            {
                switch (feature)
                {

                    case Feature.Youtube:
                        YoutubeWindow youtube = new YoutubeWindow(CurrentFeature.Content, CurrentFeature.SelectedMonitor);
                        youtube.Show();
                        featureControl.window = youtube;
                        featureControl.Content = CurrentFeature.Content;
                        featureControl.feature = feature;
                        break;
                    case Feature.Video:
                        VideoForm video = new VideoForm(videopath: CurrentFeature.Content, ownerScreenIndex: CurrentFeature.SelectedMonitor);
                        video.Show();
                        featureControl.form = video;
                        featureControl.Content = CurrentFeature.Content;
                        featureControl.feature = feature;
                        break;
                    case Feature.GIF:
                        GifForm gif = new GifForm(CurrentFeature.Content, CurrentFeature.SelectedMonitor);
                        gif.Show();
                        featureControl.form = gif;
                        featureControl.Content = CurrentFeature.Content;
                        featureControl.feature = feature;
                        break;
                }

            }
        }

        private bool CheckEmpty()
        {
            if (CurrentFeature.Content != null)
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
            FeatureControl featureControl = CurrentFeature.featurelist[CurrentFeature.SelectedMonitor];
            if (featureControl.form != null)
                featureControl.form.Close();
            if (featureControl.window != null)
                featureControl.window.Close();
        }

        private void StopAllWallpaper()
        {
            foreach (FeatureControl featureControl in CurrentFeature.featurelist)
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
        }
    }
}
