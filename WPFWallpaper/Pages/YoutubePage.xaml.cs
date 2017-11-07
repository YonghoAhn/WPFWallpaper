using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Collections.Generic;
using System.Net;
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
            YoutubeList.Add(new YoutubeSearchModel() { Name = "Test", Desc="Test", ID="Test", Path=null, Title="Test" });
        }

        private async void YoutubeSearchButton_ClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            var YoutubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBgwr13tNn2BL5TbKLvT6e_kCR8egeQ5og", // 키 지정
                ApplicationName = "wallpaperengine"
            });
            var request = YoutubeService.Search.List("snippet");
            request.Q = YoutubeSearchTextBox.Text;  //Search Keyword
            request.MaxResults = 25;
            var result = await request.ExecuteAsync();
            foreach(var item in result.Items)
            {
                if(item.Id.Kind == "youtube#video")
                {
                    var req = WebRequest.Create(item.Snippet.Thumbnails.High.Url);
                    using (var r = req.GetResponse())
                    {
                        using (var stream = r.GetResponseStream())
                        {
                            var img = System.Drawing.Image.FromStream(stream: stream);
                            
                        }
                    }
            }
        }
    }
}
