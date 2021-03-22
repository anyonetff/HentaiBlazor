using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class ImageUtils
    {

        public static string Preview(ZipArchiveEntry entry, 
            int width, int height, ImageFormat format)
        {
            Image source = Create(entry);

            Image target = ThumbnailFit(source, width, height);

            return Base64(target, format);
        }

        public static string Read(ZipArchiveEntry entry)
        {
            var stream = entry.Open();
            // byte[] bytes;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                // bytes = ms.ToArray();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Base64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);

                //byte[] bytes = ms.ToArray();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static Image Create(ZipArchiveEntry entry)
        {
            var stream = entry.Open();
            // byte[] bytes;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                // bytes = ms.ToArray();

                return Image.FromStream(ms);
            }
        }

        public static bool ThumbnailCallback()
        {
            Console.WriteLine("纳尼?");

            return false;
        }

        public static Image Thumbnail(Image source, int width, int height)
        {
            Image.GetThumbnailImageAbort callback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);

            return source.GetThumbnailImage(width, height, callback, IntPtr.Zero);
        }

        public static Image ThumbnailWidth(Image source, int width)
        {
            float ratio = ((float) source.Width) / ((float) width);

            int _width = width;
            int _height = (int) (((float)source.Height) / ratio);

            return Thumbnail(source, _width, _height);
        }

        public static Image ThumbnailHeight(Image source, int height)
        {
            float ratio = ((float)source.Height) / ((float)height);

            int _width = (int)(((float)source.Width) / ratio);
            int _height = height;

            return Thumbnail(source, _width, _height);
        }

        public static Image ThumbnailFit(Image source, int width, int height)
        {
            float ratio = (float)source.Width / (float)source.Height;
            float _ratio = (float)width / (float)height;

            if (ratio > _ratio)
            {
                return ThumbnailHeight(source, height);
            }

            return ThumbnailWidth(source, width);
        }

        public static Image ThumbnailIn(Image source, int width, int height)
        {
            float ratio = (float)source.Width / (float)source.Height;
            float _ratio = (float)width / (float)height;

            if (ratio > _ratio)
            {
                return ThumbnailWidth(source, width);
            }

            return ThumbnailHeight(source, height);
        }

    }
}
