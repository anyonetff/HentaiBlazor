using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Author
    {
        [Parameter]
        public string Id { get; set; }

        private BAuthorEntity authorEntity;

        private List<BAuthorEntity> authorEntities;

        private List<CBookEntity> bookEntities;

        [Inject]
        public AuthorService authorService { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public DrawerService _drawer { get; set; }

        private DrawerRef<string> _detailRef;

        private List<string> authors = new List<string>();

        private bool All = false;


        protected override async Task OnInitializedAsync()
        {
            authorEntity = await authorService.FindAsync(Id);

            string nameAlias = StringUtils.IsEqual(".", authorEntity.Alias) ? authorEntity.Name : authorEntity.Alias;

            authorEntities = await authorService.ListByNameAliasAsync(nameAlias);
            authorEntities.ForEach(a => { authors.Add(a.Name); });

            await Refresh();
        }


        private async Task OnAll()
        {
            All = true;

            await Refresh();
        }

        private async Task OnAuthor(string id)
        {
            All = false;

            authorEntity = await authorService.FindAsync(Id);

            await Refresh();
        }

        private async Task Refresh()
        {
            if (All)
            {
                Console.WriteLine("提取所有关联作者[" + authors.Count + "].");

                bookEntities = await bookService.ListByAuthorAsync(authors);
            }
            else
            {
                Console.WriteLine("提取单取作者.");

                bookEntities = await bookService.ListByAuthorAsync(authorEntity.Name);
            }
        }


        private async Task OpenDetail(string options, string title)
        {
            var _config = new DrawerOptions();

            _config.Title = title;
            _config.Width = 720;
            //modalConfig.Footer = null;

            _detailRef = await _drawer
                .CreateAsync<Detail, string, string>(_config, options);

            _detailRef.OnClosed = async (r) =>
            {
                Console.WriteLine(r);

                await Task.CompletedTask;
            };
        }

    }
}
