using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void RemoveCached(string id)
        {
            if (cache.ContainsKey(id))
            {
                cache.Remove(id);
            }
        }

        public async Task<byte[]> GetAsync(CBookEntity book)
        {
            //lock(this);

            if (cache.ContainsKey(book.Id))
            {
                return cache[book.Id];
            }
            //string cover = "";

            byte[] c = await ReadAsync(Path.Combine(book.Path, book.Name), book.Cover);
            cache.Add(book.Id, c);

            if (c == null || c.Length == 0)
            {
                book.Note = "err:拾取封面失败.";
            }
            
            return cache[book.Id];
        }

        // TODO: 数据回收算法
        private async Task FreeAsync()
        {

            await Task.CompletedTask;
        }

        private async Task<byte[]> ReadAsync(string file, string cover)
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
                    try
                    {
                        IArchiveEntry result = null;

                        foreach (var entry in archive.Entries)
                        {
                            if (result == null && !entry.IsDirectory && ComicUtils.IsImage(entry.Key))
                            {
                                result = entry;

                                if (StringUtils.IsBlank(cover))
                                {
                                    break;
                                }
                            }

                            if (StringUtils.IsEqual(entry.Key, cover))
                            {
                                result = entry;
                                break;
                            }
                        }

                        if (result != null)
                        {
                            return ImageUtils.PreviewBuffer(result, width, height);
                        }
                    }
                    catch (ArchiveException ex)
                    {
                        Console.WriteLine(file);
                        Console.WriteLine(ex);
                        //throw;
                    }
                    catch (CryptographicException ex)
                    {
                        Console.WriteLine(file);
                        Console.WriteLine(ex);
                    }
                }

                return none;
            });
        }

        private IEnumerable<IArchiveEntry> process(string file)
        {


            return null;
        }


    }
}
