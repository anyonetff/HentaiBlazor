using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services
{
    public class ReaderService
    {

        private Dictionary<string, string> crypto;

        private IEnumerable<IArchiveEntry> Open(CBookEntity book, List<string> secrets)
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

            foreach (var _secret in secrets)
            {
                try
                {
                    if (StringUtils.IsBlank(_secret))
                    {
                        archive = ArchiveFactory.Open(f);
                    }
                    else
                    {
                        archive = ArchiveFactory.Open(f, new ReaderOptions { Password = _secret });
                    }

                    entries = archive.Entries;
                }
                catch (ArchiveException ex)
                {
                    Console.WriteLine("压缩包存在问题.");
                    Console.WriteLine(ex);
                    break;
                }
                catch (CryptographicException ex)
                {
                    Console.WriteLine("解压密码不正确.");
                    Console.WriteLine(ex);
                }
            }

            if (entries != null)
            {

            }

            return entries;
        }


    }
}
