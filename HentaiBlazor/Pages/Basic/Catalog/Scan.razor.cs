using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Basic;
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

        private bool _scaning = false;

        private int _percent = 0;

        private BCatalogEntity catalog;

        [Inject]
        public CatalogService catalogService { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public AuthorService authorService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //catalog = new BCatalogEntity();
            catalog = base.Options ?? new BCatalogEntity();

            await base.OnInitializedAsync();
        }

        private async Task Stop()
        {
            if (_scaning)
            {
                Console.WriteLine("中止操作...");

                _scaning = false;
            }

            await Task.CompletedTask;
        }

        private async Task Start()
        {
            if (_scaning)
            {
                return;
            }

            Console.WriteLine("开始扫描文件目录...");

            _scaning = true;
            _percent = 0;

            DirectoryInfo root = new DirectoryInfo(catalog.Path);

            if (! root.Exists)
            {
                Console.WriteLine("目录不存在");
                // 文件目录不存在
                // TODO: 这里应该有个报警.
                return;
            }

            FileInfo[] files = root.GetFiles("*.zip");

            Console.WriteLine("找到[" + files.Length + "]个文件");

            int progress = 0;
            double total = files.Length;

            for (int i = 0; i < files.Length; i ++)
            {
                if (!_scaning)
                {
                    break;
                }

                FileInfo file = files[i];

                CBookEntity book = await saveBook(catalog, file);
                BAuthorEntity author = await saveAuthor(book.Author);

                progress = (int) (((double) i) / total * 100.0);

                catalog.Items = i;

                // Console.WriteLine(progress);

                if (_percent != progress)
                {
                    _percent = progress;
                    Console.WriteLine(" * " + _percent + "% * ");

                    // await Task.Delay(500);

                    StateHasChanged();
                }
                
            }

            Console.WriteLine("完成目录扫描");

            _scaning = false;

            
        }

        private async Task<CBookEntity> saveBook(BCatalogEntity catalog, FileInfo file)
        {
            CBookEntity book = await bookService.FindByNameAsync(file.DirectoryName, file.Name);

            if (book != null)
            {
                return book;
            }

            Console.WriteLine(" - " + file.FullName);

            book = new CBookEntity();

            //book.Id = Guid.NewGuid().ToString();
            //book.Catalog = catalog;

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

        private async Task<BAuthorEntity> saveAuthor(string name)
        {
            if (name == null || name == "")
            {
                return null;
            }

            BAuthorEntity author = await authorService.FindByNameAsync(name);

            if (author != null)
            {
                return author;
            }

            author = new BAuthorEntity();

            //author.Id = Guid.NewGuid().ToString();
            author.Name = name;
            author.Alias = ".";
            author.Valid = true;

            author = await authorService.SaveAsync(author);

            return author;
        }

    }
}
