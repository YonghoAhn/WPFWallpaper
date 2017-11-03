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
            switch((sender as RadioButton).Name)
            {
                case "YoutubeToggle":
                    MainFrame.Navigate(new Pages.YoutubePage());
                    break;
                case "VideoToggle":
                    MainFrame.Navigate(new Pages.VideoPage());
                    break;
                case "GifToggle":
                    MainFrame.Navigate(new Pages.GifPage());
                    break;
                case "SettingToggle":
                    MainFrame.Navigate(new Pages.SettingPage());
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            YoutubeWindow youtubeWindow = new YoutubeWindow();
            youtubeWindow.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
