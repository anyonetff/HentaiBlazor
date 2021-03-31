using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Comic;
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
        public BookService bookService { get; set; }

        private async Task DiscoveryComic(string path, bool children)
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
                    await DiscoveryComic(dir.FullName, children);
                }
            }

            FileInfo[] files = root.GetFiles();

            foreach (var file in files)
            {
                if (!ComicUtils.IsArchive(file.Name))
                {
                    // 如果当前文件不是漫画档案
                    continue;
                }

                CBookEntity book = await CreateBook(file);
                BAuthorEntity author = await CreateAuthor(book.Author);
            }
        }

        private async Task<CBookEntity> CreateBook(FileInfo file)
        {
            CBookEntity book = await bookService.FindByNameAsync(file.DirectoryName, file.Name);

            if (book != null)
            {
                Console.WriteLine("文件已存在...");

                return book;
            }

            book = new CBookEntity();

            book.Path = file.DirectoryName;
            book.Name = file.Name;

            book.Author = ComicUtils.ParseAuthor(book.Name);
            book.Title = ComicUtils.ParseTitle(book.Name);
            book.Language = ComicUtils.ParseLanguage(book.Name);

            book.Length = file.Length;

            book.XInsert_ = file.CreationTime;
            book.XUpdate_ = file.LastWriteTime;

            book = await bookService.SaveAsync(book);

            return book;
        }

    }
}
