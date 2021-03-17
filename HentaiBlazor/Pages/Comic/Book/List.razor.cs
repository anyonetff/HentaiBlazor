using AntDesign;
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

namespace HentaiBlazor.Pages.Comic.Book
{
    public partial class List
    {
        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public CatalogService catalogService { get; set; }

        [Inject]
        public AuthorService authorService { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        private ModalRef _editRef;

        private List<CBookEntity> CBookEntities;

        private string searchKeyword;

        private string searchCatalog;

        private string searchAuthor;

        protected override async Task OnInitializedAsync()
        {
            CBookEntities = await bookService.ListAsync();
        }

        private async Task OpenModify(string id)
        {
            var templateOptions = await bookService.FindAsync(id);

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenEdit(CBookEntity templateOptions)
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "编辑数据";
            modalConfig.Footer = null;

            _editRef = await _modal
                .CreateModalAsync<Edit, CBookEntity>(modalConfig, templateOptions);
        }

        public async Task Search()
        {
            Console.WriteLine(" search: " + searchKeyword);

            CBookEntities = await bookService.SearchAsync(searchCatalog, searchAuthor, searchKeyword);

            StateHasChanged();
        }

        public async Task ScanAll()
        {
            List<BCatalogEntity> catalogs = await this.catalogService.ListAsync();

            foreach (var c in catalogs)
            { 
                await scan(c);
            }
        }

        private async Task scan(BCatalogEntity catalog)
        {
            Console.WriteLine("开始扫描目录[" + catalog.Usage + ":" + catalog.Path + "]");

            DirectoryInfo root = new DirectoryInfo(catalog.Path);

            FileInfo[] files = root.GetFiles("*.zip");

            foreach (var file in files)
            {
                CBookEntity book = save(catalog, file);

                authored(book.Author);
            }

            Console.WriteLine("完成目录扫描");

            await Task.CompletedTask;
        }

        private CBookEntity save(BCatalogEntity catalog, FileInfo file)
        {
            CBookEntity book = bookService.FindByName(file.DirectoryName, file.Name);

            if (book != null) 
            {
                return book;
            }

            Console.WriteLine(" - " + file.FullName);

            book = new CBookEntity();

            book.Id = Guid.NewGuid().ToString();
            //book.Catalog = catalog;

            book.Path = file.DirectoryName;
            book.Name = file.Name;

            book.Author = authoring(book.Name);
            book.Title = titleing(book.Name);

            book.Length = file.Length;

            book.XInsert_ = DateTime.Now;
            book.XUpdate_ = DateTime.Now;

            this.bookService.Add(book);

            return book;
        }

        private string authoring(string name)
        {
            string r = "";

            int s = name.IndexOf("[");
            int e = name.IndexOf("]");

            if (s > -1 && e > 1)
            {
                r = name.Substring(s + 1, e - s - 1).Trim();
            }

            Console.WriteLine("   [ " + s + " - " + e + " ] " + r);

            return r;
        }

        private void authored(string name)
        {
            if (name == null || name == "")
            {
                return;
            }

            BAuthorEntity author = authorService.FindByName(name);

            if (author != null)
            {
                return;
            }

            author = new BAuthorEntity();

            author.Id = Guid.NewGuid().ToString();
            author.Name = name;
            author.Alias = ".";
            author.Valid = true;

            authorService.Add(author);
        }

        private string titleing(string name)
        {
            string r = "";

            int s = name.IndexOf("]");
            int e = name.LastIndexOf(".");

            if (s > -1 && e > 1)
            {
                r = name.Substring(s + 1, e - s - 1).Trim();
            }

            Console.WriteLine("   [ " + s + " - " + e + " ] " + r);

            return r;
        }

    }
}
