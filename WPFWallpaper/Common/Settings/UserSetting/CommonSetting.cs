using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWallpaper.Models;

namespace WPFWallpaper.Common.Settings.UserSetting
{
    public class CommonSetting
    {
        private static string title;

        //private Feature feature = Feature.Empty;
        public Feature CurrentFeature { get; set; }
        public int CurrentMonitor { get; set; }
        public string CurrentContent { get; set; }
        public bool RunAtStartup { get; set; }
        public int Volume { get; set; }
        public string Title { get { return title; } set { title = value; MainWindow.FeatureContent = value; } }
    }
}
