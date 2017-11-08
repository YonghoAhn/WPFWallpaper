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
        static ObservableCollection<YoutubeSearchModel> YoutubeCollection = new ObservableCollection<YoutubeSearchModel>();

        public YoutubePage()
        {
            InitializeComponent();
        }

        private async void YoutubeSearchButton_ClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            
               YoutubeCollection = await YoutubeManager.SearchYoutubeAsync(YoutubeSearchTextBox.Text);
            YoutubeSearchListbox.ItemsSource = YoutubeCollection;
        }

        private void YoutubeSearchListbox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (YoutubeSearchListbox.SelectedItems.Count > 0)
            {
                YoutubeSearchModel model = YoutubeCollection[YoutubeSearchListbox.SelectedIndex];
                SelectedImage.Source = model.Path;
                SelectedTitle.Text = model.Title;
                SelectedDesc.Text = model.Desc;
            }
        }
    }
}
