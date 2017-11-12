﻿using System;
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

namespace WPFWallpaper.Pages
{
    /// <summary>
    /// VideoPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VideoPage : Page
    {
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
    }
}
