using System.Collections.Generic;
using System.Windows.Controls;
using WPFWallpaper.Models;
namespace WPFWallpaper.Pages
{
    /// <summary>
    /// YoutubePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YoutubePage : Page
    {
        static List<YoutubeSearchModel> YoutubeList = new List<YoutubeSearchModel>();

        public YoutubePage()
        {
            InitializeComponent();
            YoutubeSearchListbox.ItemsSource = YoutubeList;
            YoutubeList.Add(new YoutubeSearchModel() { Name = "Test", Desc="Test", ID="Test", Path="Test", Title="Test" });
        }
    }
}
