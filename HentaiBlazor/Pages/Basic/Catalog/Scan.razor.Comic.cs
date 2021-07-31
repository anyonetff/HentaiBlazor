using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Scan
    {

        [Inject]
        public BookService bookService { get; set; }

        private async Task<int> DiscoveryComic(string path, bool children, DateTime refresh)
        {
            DirectoryInfo root = new DirectoryInfo(path);

            if (!root.Exists)
            {
                Console.WriteLine("目录不存在");
                // 文件目录不存在
                // TODO: 这里应该有个报警.
                return 0;
            }

            if (children)
            {
                DirectoryInfo[] dirs = root.GetDirectories();

                foreach (var dir in dirs)
                {
                    await DiscoveryComic(dir.FullName, children, refresh);
                }
            }

            FileInfo[] files = root.GetFiles();

            this._total = _total + files.Length;

            foreach (var file in files)
            {
                this._current += 1;

                int _p = this._current * 100 / this._total;

                if (Math.Abs(_p - this._percent) >= 5) 
                {
                    Console.WriteLine("当前进度:" + _percent);

                    this._percent = _p;

                    await InvokeAsync(StateHasChanged);
                }

                if (!ComicUtils.IsArchive(file.Name) || file.LastWriteTime.CompareTo(refresh) < 0)
                {
                    // 如果当前文件不是漫画档案
                    
                    continue;
                }

                CBookEntity book = await CreateBook(file);
                BAuthorEntity author = await CreateAuthor(book.Author);
            }

            int items = await this.bookService.TotalCountAsync(path, children);

            return items;
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
