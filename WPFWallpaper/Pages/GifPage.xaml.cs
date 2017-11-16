using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFWallpaper.Common;
using WPFWallpaper.Common.Settings;

namespace WPFWallpaper.Pages
{
    /// <summary>
    /// GifPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GifPage : Page
    {
        public GifPage()
        {
            InitializeComponent();
            VideoList.ItemsSource = PlayLists.GifLists;
        }

        private void VideoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoList.SelectedItems.Count > 0 && VideoList.SelectedItem != null)
            {
                PreviewVideo.Source = new Uri(VideoList.SelectedItem.ToString());
                SettingManager.commonSetting.CurrentContent = VideoList.SelectedItem.ToString();
                SettingManager.gifSetting.CurrentGIF = VideoList.SelectedItem.ToString();
            }
            else
            {
                PreviewVideo.Source = null;
            }

        }

        private void AddPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Open GIF",
                Filter = "GIF Format|*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PlayLists.GifLists.Add(openFileDialog.FileName);
                SettingManager.commonSetting.CurrentFeature = Models.Feature.GIF;
                SettingManager.commonSetting.CurrentContent = openFileDialog.FileName;
                SettingManager.commonSetting.Title = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                PreviewVideo.Source = new Uri(openFileDialog.FileName);
                SettingManager.gifSetting.CurrentGIF = openFileDialog.FileName;
            }
        }

        private void RemovePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoList.SelectedItems.Count > 0 && VideoList.SelectedItem != null)
            {
                PlayLists.GifLists.Remove(VideoList.SelectedItem.ToString());
            }
        }
    }
}
