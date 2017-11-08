using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFWallpaper.Common
{
    class BitmapStreamConverter
    {
        /// <summary>
        /// 스트림을 이미지로 변환합니다.
        /// </summary>
        /// <param name="stream">System.IO.Stream입니다. Image 원본 Stream입니다.</param>
        /// <returns></returns>
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
    }
}
