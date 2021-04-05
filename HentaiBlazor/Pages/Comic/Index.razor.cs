using AntDesign;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Index
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public DrawerService _drawer { get; set; }

        [Inject]
        public CoverService coverService { get; set; }

        private List<CBookEntity> CBookEntities;

        private List<CBookEntity> _CBookEntities;

        private Paginator<CBookEntity> BookPaginator = new Paginator<CBookEntity>(20);

        private DrawerRef<string> _detailRef;

        private string searchKeyword = "";

        private string searchCatalog = "";

        private string searchAuthor = "";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Search();
                StateHasChanged();
            }
        }

        public async Task Search()
        {
            Console.WriteLine(" search: " + searchKeyword);

            CBookEntities = await bookService.SearchAsync(searchCatalog, searchAuthor, searchKeyword);

            BookPaginator.DataSource = CBookEntities;
            _CBookEntities = BookPaginator.Paged().ToList();
        }

        public async Task _paging(PaginationEventArgs args)
        {
            foreach (var b in _CBookEntities)
            {
                b.Cover_ = null;
            }

            await BookPaginator.HandlePageIndexChange(args);

            _CBookEntities = BookPaginator.Paged().ToList();

            //StateHasChanged();

            //await Refresh();
        }

        public async Task _sizing(PaginationEventArgs args)
        {
            await BookPaginator.HandlePageSizeChange(args);

            _CBookEntities = BookPaginator.Paged().ToList();
        }


        private async Task OpenDetail(string options, string title)
        {
            var _config = new DrawerOptions();

            _config.Title = title;
            _config.Width = 800;
            //modalConfig.Footer = null;

            _detailRef = await _drawer
                .CreateAsync<Detail, string, string>(_config, options);

            _detailRef.OnClosed = async (r) =>
            {
                Console.WriteLine(r);

                await Task.CompletedTask;
            };
        }

        public void NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }

    }
}
