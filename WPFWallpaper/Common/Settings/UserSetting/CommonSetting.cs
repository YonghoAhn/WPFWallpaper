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
        private Feature feature = Feature.Empty;
        public string CurrentFeature { get { return Converter.ConvertFeatureToString(feature); } set {
                feature = Converter.ConvertStringToFeature(value);
            } }
        public int CurrentMonitor { get; set; }
        public string CurrentContent { get; set; }
        public bool RunAtStartup { get; set; }
        public int Volume { get; set; }
    }
}
