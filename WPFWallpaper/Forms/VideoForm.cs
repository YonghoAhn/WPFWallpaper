using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPFWallpaper.Forms
{
    public partial class VideoForm : BaseForm
    {
        public VideoForm(string videopath, int ownerScreenIndex = 1) : base(ownerScreenIndex)
        {
            InitializeComponent();
            WindowsMediaPlayer1.URL = videopath;
            WindowsMediaPlayer1.settings.setMode("loop", true);
            WindowsMediaPlayer1.Ctlcontrols.play();
        }
    }
}
