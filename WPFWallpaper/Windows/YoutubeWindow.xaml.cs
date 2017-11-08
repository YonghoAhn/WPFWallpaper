namespace WPFWallpaper.Windows
{
    /// <summary>
    /// YoutubeWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YoutubeWindow : BaseWindow
    {
        
        public YoutubeWindow(string url, int ownerScreenIndex = 0)
        {
            InitializeComponent();
            WebBrowser1.Navigate(url);
        }
    }
}
