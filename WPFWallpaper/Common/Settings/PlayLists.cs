using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWallpaper.Models;

namespace WPFWallpaper.Common.Settings
{
    class PlayLists
    {
        public static ObservableCollection<string> VideoLists = new ObservableCollection<string>();
        public static ObservableCollection<string> GifLists = new ObservableCollection<string>();
        public static ObservableCollection<YoutubeSearchModel> YoutubeCollection = new ObservableCollection<YoutubeSearchModel>();
        public static ObservableCollection<FeatureControl> QuickCollection = new ObservableCollection<FeatureControl>();
        public static ObservableCollection<string> MusicLists = new ObservableCollection<string>();

    }
}
