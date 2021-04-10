using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services
{
    // 用于封面预览的类
    // 这是一个单例模型可以缓存最近翻页查询过的封面
    // 不过这里没有编写回收算法
    public class CoverService
    {
        
        private static byte [] none = new byte [0];

        private static int width = 200;

        private static int height = 400;

        // 封面缓存
        // private Dictionary<string, string> cache;

        private Dictionary<string, byte []> cache;

        public CoverService()
        {
            cache = new Dictionary<string, byte []>();
        }

        public byte[] GetCached(string id)
        {
            if (cache.ContainsKey(id))
            {
                return cache[id];
            }

            return null;
        }

        public async Task<byte[]> GetAsync(CBookEntity book)
        {
            //lock(this);

            if (cache.ContainsKey(book.Id))
            {
                return cache[book.Id];
            }
            //string cover = "";

            byte[] c = await ReadAsync(Path.Combine(book.Path, book.Name));

            cache.Add(book.Id, c);

            return c;
        }

        // TODO: 数据回收算法
        private async Task FreeAsync()
        {

            await Task.CompletedTask;
        }

        private async Task<byte[]> ReadAsync(string file)
        {
            return await Task<byte[]>.Run(() =>
            {
                FileInfo f = new FileInfo(file);

                if (!f.Exists)
                {
                    Console.WriteLine("文件不存在...");
                    return none;
                }
                
                using (var archive = ArchiveFactory.Open(file))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory && ComicUtils.IsImage(entry.Key))
                        {
                            Console.WriteLine("找到了一个图片");
                            byte [] preview = ImageUtils.PreviewBuffer(entry, width, height);

                            // return (StringUtils.IsBlank(preview)) ? none : base64 + preview;
                            return preview;
                        }
                    }
                }

                return none;
            });
        }


    }
}
