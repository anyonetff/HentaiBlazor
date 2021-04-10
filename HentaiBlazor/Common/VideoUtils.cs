using LibVLCSharp.Shared;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class VideoUtils
    {

        public static Image Thumbnail(string file, int width, int height)
        {
            FileInfo f = new FileInfo(file);

            if (!f.Exists)
            {
                Console.WriteLine("文件不存在...");
                return null;
            }


            Core.Initialize();

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                // mediaPlayer.
                
            }

            return null;
        }

    }
}
