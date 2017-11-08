using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFWallpaper.Models;

namespace WPFWallpaper.Common
{
    class YoutubeManager
    {
        public static async Task<ObservableCollection<YoutubeSearchModel>> SearchYoutubeAsync(string keyword)
        {
            ObservableCollection<YoutubeSearchModel> collection = new ObservableCollection<YoutubeSearchModel>();
            var YoutubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBgwr13tNn2BL5TbKLvT6e_kCR8egeQ5og", // 키 지정
                ApplicationName = "wallpaperengine"
            });
            var request = YoutubeService.Search.List("snippet");
            request.Q = keyword;  //Search Keyword
            request.MaxResults = 25;
            var result = await request.ExecuteAsync();
            foreach (var item in result.Items)
            {
                if (item.Id.Kind == "youtube#video")
                {
                    var req = WebRequest.Create(item.Snippet.Thumbnails.High.Url);
                    using (var r = await req.GetResponseAsync())
                    {
                        using (var stream = r.GetResponseStream())
                        {
                            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                            BitmapImage bitmapimage = BitmapStreamConverter.ConvertImageToBitmap(image);
                            collection.Add(new YoutubeSearchModel() { Title = item.Snippet.Title, Desc = item.Snippet.Description, ID = item.Id.VideoId, Path = bitmapimage });

                        }
                    }
                }
            }
            return collection;
        }
            
    }
}
