using HentaiBlazor.Data.Anime;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Controllers
{
    // 视频服务控制器
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoService videoService;

        private readonly static int bufferSize = 1024;

        public VideoController(VideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpGet("Playback/{id}")]
        public async Task Playback(string id)
        {
            Console.WriteLine("开始播放视频文件[" + id + "].");

            AVideoEntity video = await videoService.FindAsync(id);

            if (video == null)
            {
                return;
            }
            
            using (Stream stream = this.Response.BodyWriter.AsStream())
            {
                await WriteToStream(stream, Path.Combine(video.Path, video.Name));
            }

        }


        public async Task WriteToStream(Stream outputStream, string file)
        {

            byte[] buffer = new byte[bufferSize];
            //here we re using FileStream to read file from server//  
            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int totalSize = (int)fileStream.Length;
                /*here we are saying read bytes from file as long as total size of file 

                is greater then 0*/
                while (totalSize > 0)
                {
                    int count = totalSize > bufferSize ? bufferSize : totalSize;
                    //here we are reading the buffer from orginal file  
                    int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                    //here we are writing the readed buffer to output//  
                    await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                    //and finally after writing to output stream decrementing it to total size of file.  
                    totalSize -= sizeOfReadedBuffer;
                }
            }
        }

    }
}
