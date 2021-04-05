using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class ImageUtils
    {
        // 通过压缩流生成预览图片
        public static string PreviewBase64(IArchiveEntry entry, 
            int width, int height, ImageFormat format)
        {
            Image source = Create(entry);
            if (source == null)
            {
                return "";
            }

            Image target = ThumbnailOut(source, width, height);

            return WriteToBase64(target, format);
        }

        public static byte [] PreviewBuffer(IArchiveEntry entry,
            int width, int height, ImageFormat format)
        {
            Image source = Create(entry);
            if (source == null)
            {
                return null;
            }

            Image target = ThumbnailOut(source, width, height);

            return WriteToBuffer(target, format);
        }


        // 将压缩流还原成图片的base64数据
        public static string Read(IArchiveEntry entry)
        {
            try
            {
                if (entry.IsEncrypted)
                {
                    return null;
                }

                using (var stream = entry.OpenEntryStream())
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        // bytes = ms.ToArray();

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("创建图片出现异常:");
                Console.WriteLine(e);
            }

            return null;
        }

        // 将图片编码成base64数据
        // 如果要拼接成dataURL，可以自己在前面加
        public static string WriteToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);

                //byte[] bytes = ms.ToArray();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static byte[] WriteToBuffer(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);

                // byte[] bytes = ms.ToArray();

                return ms.ToArray();
            }
        }

        public static Stream WriteToStream(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);

                return ms;
            }
        }

        // 通过压缩流创建图片
        public static Image Create(IArchiveEntry entry)
        {
            try
            {
                if (entry.IsEncrypted)
                {
                    return null;
                }

                using (var stream = entry.OpenEntryStream()) 
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        // bytes = ms.ToArray();

                        return Image.FromStream(ms);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("创建图片出现异常:");
                Console.WriteLine(e);
            }

            return null;
        }

        // 这是一个终止回调函数
        public static bool ThumbnailCallback()
        {
            Console.WriteLine("纳尼?");

            return false;
        }

        // 生成指定尺寸的缩略图
        public static Image Thumbnail(Image source, int width, int height)
        {
            Image.GetThumbnailImageAbort callback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);

            return source.GetThumbnailImage(width, height, callback, IntPtr.Zero);
        }

        // 适应宽度生成缩略图
        public static Image ThumbnailWidth(Image source, int width)
        {
            float ratio = ((float) source.Width) / ((float) width);

            int _width = width;
            int _height = (int) (((float)source.Height) / ratio);

            return Thumbnail(source, _width, _height);
        }

        // 适应高度生成缩略图
        public static Image ThumbnailHeight(Image source, int height)
        {
            float ratio = ((float)source.Height) / ((float)height);

            int _width = (int)(((float)source.Width) / ratio);
            int _height = height;

            return Thumbnail(source, _width, _height);
        }

        // 自动判定高宽比率，生成缩略图
        // 填满在指定高宽内
        public static Image ThumbnailOut(Image source, int minWidth, int minHeight)
        {
            float ratio = (float)source.Width / (float)source.Height;
            float _ratio = (float)minWidth / (float)minHeight;

            if (ratio > _ratio)
            {
                return ThumbnailHeight(source, minHeight);
            }

            return ThumbnailWidth(source, minWidth);
        }

        // 自动判定高宽比率，生成缩略图
        // 包含在指定高宽内
        public static Image ThumbnailIn(Image source, int maxWidth, int maxHeight)
        {
            float ratio = (float)source.Width / (float)source.Height;
            float _ratio = (float)maxWidth / (float)maxHeight;

            if (ratio > _ratio)
            {
                return ThumbnailWidth(source, maxWidth);
            }

            return ThumbnailHeight(source, maxHeight);
        }

    }
}
