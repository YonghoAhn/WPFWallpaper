using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPFWallpaper.Pages
{
    /// <summary>
    /// VideoPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VideoPage : Page
    {
        bool IsPlaying = false;
        bool IsMuted = false;
        public VideoPage()
        {
            InitializeComponent();
        }

        private void AddPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Open Video",
                Filter = "MPEG Format|*.mpeg;*.mpg|AVI Format|*.avi|MP4 Format|*.mp4"
            };
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                VideoList.Items.Add(openFileDialog.FileName);
                CurrentFeature.feature = Models.Feature.Video;
                CurrentFeature.Content = openFileDialog.FileName;
                PreviewVideo.Source = new Uri(openFileDialog.FileName);
            }
        }

        private void RemovePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if(VideoList.SelectedItems.Count > 0)
            {
                foreach(var item in VideoList.SelectedItems)
                {
                    VideoList.Items.RemoveAt(VideoList.SelectedItems.IndexOf(item));
                }
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if(!IsPlaying && PreviewVideo.Source!=null)
            {
                Console.WriteLine(PreviewVideo.Source);
                PreviewVideo.Play();
                //Console.WriteLine(File.Exists("/WPFWallpaper;component/Images/Icons/Gray/Pause.png"));
                //Console.WriteLine(new Uri("/WPFWallpaper;component/Images/Icons/Gray/Pause.png",UriKind.Relative));
                PlayButtonBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WPFWallpaper;component/Images/Icons/Gray/Pause.png"));
                IsPlaying = true;
                Console.WriteLine(PreviewVideo.Source);
            }
            else if(IsPlaying && PreviewVideo.CanPause)
            {
                PreviewVideo.Pause();
                IsPlaying = false;
                PlayButtonBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WPFWallpaper;component/Images/Icons/Gray/Play.png"));

            }
        }

        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsMuted)
            {
                PreviewVideo.Volume = 100;
                IsMuted = false;
                VolumeButtonBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WPFWallpaper;component/Images/Icons/Gray/Volume.png"));
            }
            else
            {
                PreviewVideo.Volume = 0;
                IsMuted = true;
                VolumeButtonBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WPFWallpaper;component/Images/Icons/Gray/Mute.png"));
            }
        }

        private void VideoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoList.SelectedItems.Count > 0)
                PreviewVideo.Source = new Uri(VideoList.SelectedItem.ToString());
            Console.WriteLine(File.Exists(VideoList.SelectedItem.ToString()));
        }
    }
}
