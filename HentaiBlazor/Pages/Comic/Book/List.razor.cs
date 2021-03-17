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
            await scan(catalogs.ElementAt(0));
        }

        private async Task scan(BCatalogEntity catalog)
        {
            DirectoryInfo root = new DirectoryInfo(catalog.Path);

            FileInfo[] files = root.GetFiles("*.zip");

            foreach (var file in files)
            {
                Console.WriteLine(" - " + file.FullName);

                CBookEntity book = save(catalog, file);
            }

            await Task.CompletedTask;
        }

        private CBookEntity save(BCatalogEntity catalog, FileInfo file)
        {
            CBookEntity book = new CBookEntity();

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
