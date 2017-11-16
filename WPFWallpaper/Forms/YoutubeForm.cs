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
    public partial class YoutubeForm : BaseForm
    {
        public YoutubeForm(string URL, int ownerScreenIndex = 0) : base(ownerScreenIndex)
        {
            InitializeComponent();
            webBrowser1.Navigate(URL,null, null, "Referer:https://youtube.com/");
        }

        private void YoutubeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
