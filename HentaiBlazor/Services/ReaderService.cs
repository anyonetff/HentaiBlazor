using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiBlazor.Services
{
    public class ReaderService
    {

        public IArchiveEntry Cover(CBookEntity book, List<BCryptoEntity> cryptos)
        {
            IEnumerable<IArchiveEntry> entries = Open(book, cryptos);

            if (entries == null || ! entries.Any())
            {
                return null;
            }

            IArchiveEntry result = null;

            foreach (var entry in entries)
            {
                // 如果文件没有指定封面.
                if (result == null && !entry.IsDirectory && ComicUtils.IsImage(entry.Key))
                {
                    result = entry;

                    if (StringUtils.IsBlank(book.Cover))
                    {
                        break;
                    }
                }

                // 如果文件指定了封面.
                if (StringUtils.IsEqual(entry.Key, book.Cover))
                {
                    result = entry;
                    break;
                }
            }

            return result;
        }

        public List<IArchiveEntry> Images(CBookEntity book, List<BCryptoEntity> cryptos)
        {
            IEnumerable<IArchiveEntry> entries = Open(book, cryptos);

            if (entries == null || !entries.Any())
            {
                return new List<IArchiveEntry>();
            }

            return entries.Where(a => (!a.IsDirectory && ComicUtils.IsImage(a.Key)))
                    .OrderBy(a => a.Key)
                    .ToList();
        }

        private IEnumerable<IArchiveEntry> Open(CBookEntity book, List<BCryptoEntity> cryptos)
        {
            string file = Path.Combine(book.Path, book.Name);

            FileInfo f = new FileInfo(file);

            if (!f.Exists)
            {
                Console.WriteLine("文件不存在...");
                return null;
            }

            IArchive archive = null;
            IEnumerable<IArchiveEntry> entries = null;

            foreach (var crypto in cryptos)
            {
                try
                {
                    ReaderOptions readerOptions = new ReaderOptions { LeaveStreamOpen = false };

                    if (StringUtils.IsNotBlank(crypto.Secret)) 
                    {
                        Console.WriteLine("解压密码:" + crypto.Secret);
                        readerOptions.Password = crypto.Secret;
                    }

                    ArchiveEncoding archiveEncoding = new ArchiveEncoding(Encoding.UTF8, Encoding.UTF8);
                    readerOptions.ArchiveEncoding = archiveEncoding;

                    archive = ArchiveFactory.Open(f, readerOptions);
                    entries = archive.Entries;

                    entries.Any();
                }
                catch (ArchiveException ex)
                {
                    Console.WriteLine("压缩包存在问题.");
                    Console.WriteLine(ex);

                    entries = null;

                    // break;
                }
                catch (InvalidFormatException ex)
                {
                    Console.WriteLine("压缩包格式存在问题.");
                    Console.WriteLine(ex);

                    entries = null;

                    // break;
                }
                catch (CryptographicException ex)
                {
                    Console.WriteLine("解压密码不正确.");
                    Console.WriteLine(ex);

                    entries = null;
                }
            }

            return entries;
        }


    }
}
