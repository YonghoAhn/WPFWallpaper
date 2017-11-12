using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWallpaper.Models;

namespace WPFWallpaper.Common
{
    class CurrentFeature
    {
        public static ObservableCollection<FeatureControl> featurelist = new ObservableCollection<FeatureControl>();
        public static int SelectedMonitor = 0;
        public static string Content;
        static Feature _feature;
        public static Feature feature { get { return _feature; } set { _feature = value;  } }

        static string title;
        public static string Title { get { return title;  } set { title = value; MainWindow.FeatureContent = value; } }
    }
}
