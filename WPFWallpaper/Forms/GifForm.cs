using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFWallpaper.Controls;

namespace WPFWallpaper.Forms
{
    public partial class GifForm : BaseForm
    {
        public GifForm(string gifpath)
        {
            InitializeComponent();
            GifImage gif = new GifImage(gifpath)
            {
                ReverseAtEnd = false
            };
            pictureBox1.Image = gif.GetNextFrame();
        }
    }
}
