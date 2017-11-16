using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPFWallpaper.Common;
using WPFWallpaper.Common.Settings;
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
            SelectedTitle.Text = SettingManager.youtubeSetting.CurrentYoutubeTitle;
            SelectedDesc.Text = SettingManager.youtubeSetting.CurrentYoutubeDesc;
            try
            {
                SelectedImage.Source = Converter.ConvertImageToBitmap(System.Drawing.Image.FromFile(SettingManager.Youtube_Path));
            }
            catch (Exception)
            {
                SelectedImage.Source = null;
            }
        }

        private async void YoutubeSearchButton_ClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            PlayLists.YoutubeCollection = await YoutubeManager.SearchYoutubeAsync(YoutubeSearchTextBox.Text);
            YoutubeSearchListbox.ItemsSource = PlayLists.YoutubeCollection;
        }

        private void YoutubeSearchListbox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectionItemChanged();
        }

        private void YoutubeSearchListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionItemChanged();
        }

        private void SelectionItemChanged()
        {
            if (YoutubeSearchListbox.SelectedItems.Count > 0)
            {
                YoutubeSearchModel model = PlayLists.YoutubeCollection[YoutubeSearchListbox.SelectedIndex];
                SelectedImage.Source = model.Path;
                Converter.SaveImage(model.Path, SettingManager.Youtube_Path);
                SelectedTitle.Text = model.Title;
                SettingManager.commonSetting.Title = model.Title;
                SelectedDesc.Text = model.Desc;
                SettingManager.commonSetting.CurrentContent = "https://youtube.com/embed/" + model.ID + "?autoplay=1&loop=1&controls=0&showinfo=0&vq=hd1080&playlist=" + model.ID;

                SettingManager.youtubeSetting.CurrentYoutubeTitle = model.Title;
                SettingManager.youtubeSetting.CurrentYoutubeDesc = model.Desc;
                SettingManager.youtubeSetting.CurrentYoutubeContent = SettingManager.commonSetting.CurrentContent;
            }
        }
    }
}
