using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPFWallpaper.Common;
using WPFWallpaper.Models;
namespace WPFWallpaper.Pages
{
    /// <summary>
    /// YoutubePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YoutubePage : Page
    {
        public YoutubePage()
        {
            InitializeComponent();
        }

        private async void YoutubeSearchButton_ClickAsync(object sender, System.Windows.RoutedEventArgs e) => YoutubeSearchListbox.ItemsSource = await YoutubeManager.SearchYoutubeAsync(YoutubeSearchTextBox.Text);
    }
}
