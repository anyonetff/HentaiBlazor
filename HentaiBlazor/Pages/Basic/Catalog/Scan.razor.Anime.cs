using HentaiBlazor.Common;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Scan
    {

        [Inject]
        public VideoService videoService { get; set; }

        private async Task DiscoveryAnime(string path, bool children)
        {
            DirectoryInfo root = new DirectoryInfo(path);

            if (!root.Exists)
            {
                Console.WriteLine("目录不存在");
                // 文件目录不存在
                // TODO: 这里应该有个报警.
                return;
            }

            if (children)
            {
                DirectoryInfo[] dirs = root.GetDirectories();

                foreach (var dir in dirs)
                {
                    await DiscoveryAnime(dir.FullName, children);
                }
            }

            FileInfo[] files = root.GetFiles();

            foreach (var file in files)
            {
                if (!AnimeUtils.IsMedia(file.Name))
                {
                    // 如果当前文件不是视频档案
                    continue;
                }

                AVideoEntity video = await CreateVideo(file);
                //BAuthorEntity author = await CreateAuthor(book.Author);
            }
        }

        private async Task<AVideoEntity> CreateVideo(FileInfo file)
        {
            AVideoEntity video = await videoService.FindByNameAsync(file.DirectoryName, file.Name);

            if (video != null)
            {
                Console.WriteLine("文件已存在...");

                return video;
            }

            video = new AVideoEntity();

            video.Path = file.DirectoryName;
            video.Name = file.Name;

            video.Author = "";
            video.Title = AnimeUtils.ParseTitle(video.Name);
            video.Language = "ja";

            video.Length = file.Length;

            video.XInsert_ = file.CreationTime;
            video.XUpdate_ = file.LastWriteTime;

            video = await videoService.SaveAsync(video);

            return video;
        }
    }
}
