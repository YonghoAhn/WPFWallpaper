using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFWallpaper.Models;

namespace WPFWallpaper.Common
{
    class Converter
    {
        /// <summary>
        /// 스트림을 이미지로 변환합니다.
        /// </summary>
        /// <param name="stream">System.IO.Stream입니다. Image 원본 Stream입니다.</param>
        /// <returns>BitmapImage형태의 Resource를 반환합니다.</returns>
        public static BitmapImage ConvertStreamToBitmap(Stream stream)
        {
            using (var imgstream = new MemoryStream())
            {
                stream.CopyTo(imgstream); //System.IO.Stream Copy to a new MemoryStream.
                var bitmap = new BitmapImage(); //New BitmapImage
                bitmap.BeginInit();
                bitmap.StreamSource = imgstream; //Image Stream
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap; //return it
            }
        }

        public static BitmapImage ConvertImageToBitmap(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;

            }
        }

        public static Feature ConvertStringToFeature(string value)
        {
            Feature feature;
            switch (value)
            {
                case "Youtube":
                    feature = Feature.Youtube;
                    break;
                case "Video":
                    feature = Feature.Video;
                    break;
                case "Gif":
                    feature = Feature.GIF;
                    break;
                default:
                    feature = Feature.Empty;
                    break;
            }
            return feature;
        }

        public static string ConvertFeatureToString(Feature feature)
        {
            string value;
            switch (feature)
            {
                case Feature.Youtube:
                    value = "Youtube";
                    break;
                case Feature.Video:
                    value = "Video";
                        break;
                case Feature.GIF:
                    value = "Gif";
                        break;
                default:
                    value = "Empty";
                        break;
            }
            return value;
        }

    }
}
