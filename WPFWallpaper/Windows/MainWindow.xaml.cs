using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using WPFWallpaper.Forms;
using WPFWallpaper.Models;
using WPFWallpaper.Windows;

namespace WPFWallpaper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
           
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
                    MainFrame.Navigate(new Pages.YoutubePage());
                    CurrentFeature.featurelist.ElementAt(CurrentFeature.SelectedMonitor).feature = Feature.Youtube;
                    CurrentFeatureLabel.Content = "Youtube";
                    break;
                case "VideoToggle":
                    MainFrame.Navigate(new Pages.VideoPage());
                    CurrentFeature.featurelist.ElementAt(CurrentFeature.SelectedMonitor).feature = Feature.Video;
                    CurrentFeatureLabel.Content = "Video";
                    break;
                case "GifToggle":
                    MainFrame.Navigate(new Pages.GifPage());

                    CurrentFeature.featurelist.ElementAt(CurrentFeature.SelectedMonitor).feature = Feature.GIF;
                    CurrentFeatureLabel.Content = "Gif";
                    break;
                case "SettingToggle":
                    MainFrame.Navigate(new Pages.SettingPage());

                    CurrentFeature.featurelist.ElementAt(CurrentFeature.SelectedMonitor).feature = Feature.Empty;
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
            CurrentFeature.featurelist.Add(new FeatureControl() { form=null, window=null, Content="", feature = Feature.Youtube, monitor = 0 });
            //Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "WPFWallpaper.exe", 11001);
            MonitorCombo.SelectedIndex = 0;
        }

        private void MonitorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentFeature.SelectedMonitor = MonitorCombo.SelectedIndex;
            if (CurrentFeature.SelectedMonitor == -1)
                CurrentFeature.SelectedMonitor = 0;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            FeatureControl featureControl = CurrentFeature.featurelist[CurrentFeature.SelectedMonitor];
            Feature feature = featureControl.feature;
            if (featureControl.form != null)
                featureControl.form.Close();
            if (featureControl.window != null)
                featureControl.window.Close();
            switch(feature)
            {
                case Feature.Youtube:
                    YoutubeWindow youtube = new YoutubeWindow(CurrentFeature.Content, CurrentFeature.SelectedMonitor);
                    youtube.Show();
                    CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].Content = CurrentFeature.Content;
                    CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].window = youtube;
                    break;
                case Feature.Video:
                    VideoForm video = new VideoForm(videopath: CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].Content, ownerScreenIndex: CurrentFeature.SelectedMonitor);
                    video.Show();
                    CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].form = video;
                    break;
                case Feature.GIF:
                    GifForm gif = new GifForm(CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].Content, CurrentFeature.SelectedMonitor);
                    gif.Show();
                    CurrentFeature.featurelist[CurrentFeature.SelectedMonitor].form = gif;
                    break;
            }
        }
    }
}
