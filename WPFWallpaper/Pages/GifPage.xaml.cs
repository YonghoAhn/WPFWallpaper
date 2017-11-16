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
    /// GifPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GifPage : Page
    {
        public GifPage()
        {
            InitializeComponent();
        }

        private void VideoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoList.SelectedItems.Count > 0 && VideoList.SelectedItem != null)
            {
                PreviewVideo.Source = new Uri(VideoList.SelectedItem.ToString());
                CurrentFeature.Content = VideoList.SelectedItem.ToString();
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
                VideoList.Items.Add(openFileDialog.FileName);
                CurrentFeature.feature = Models.Feature.GIF;
                CurrentFeature.Content = openFileDialog.FileName;
                PreviewVideo.Source = new Uri(openFileDialog.FileName);
            }
        }

        private void RemovePlaylistButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
