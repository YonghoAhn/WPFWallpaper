using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWallpaper.Common.Settings.UserSetting
{
    public class GifSetting
    {
        public string[] GIFs;
        private string gifList;
        public string GIFList { get { return gifList; } set { gifList = value; GIFs = gifList.Split('|'); } }
        public string CurrentGIF { get; set; }
    }
}
