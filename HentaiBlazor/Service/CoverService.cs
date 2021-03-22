using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service
{
    // 用于封面预览的类
    // 这是一个单例模型可以缓存最近翻页查询过的封面
    // 不过这里没有编写回收算法
    public class CoverService
    {
        private static string cover = "/book/cover.jpg";

        private static string base64 = "data:image/*;base64,";

        private static int width = 200;

        private static int height = 400;

        private Dictionary<string, string> covers;

        public CoverService()
        {
            covers = new Dictionary<string, string>();
        }

        public async Task<string> GetAsync(CBookEntity book)
        {
            if (covers.ContainsKey(book.Id))
            {
                return covers[book.Id];
            }

            string c = await ReadAsync(book.Path + "\\" + book.Name);

            covers.Add(book.Id, c);

            return c;
        }

        // TODO: 数据回收算法
        private async Task FreeAsync()
        {

            await Task.CompletedTask;
        }

        private async Task<string> ReadAsync(string file)
        {
            return await Task<string>.Run(() =>
            {
                FileInfo f = new FileInfo(file);

                if (!f.Exists)
                {
                    Console.WriteLine("文件不存在...");
                    return cover;
                }

                ZipArchive archive = ZipFile.OpenRead(file);

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (ComicUtils.IsImage(entry.FullName))
                    {
                        Console.WriteLine("找到了一个图片");

                        return base64 + ImageUtils.Preview(entry, width, height, ImageFormat.Png);
                    }
                }

                return cover;
            });
        }


    }
}
