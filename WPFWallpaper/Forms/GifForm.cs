using WPFWallpaper.Controls;

namespace WPFWallpaper.Forms
{
    public partial class GifForm : BaseForm
    {
        public GifForm(string gifpath, int ownerScreenIndex = 0) : base(ownerScreenIndex)
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
