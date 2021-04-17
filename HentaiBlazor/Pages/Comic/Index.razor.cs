using AntDesign;
using HentaiBlazor.Common;
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

        private List<CBookEntity> CBookEntities;

        private List<CBookEntity> _CBookEntities;

        private Paginator<CBookEntity> BookPaginator = new Paginator<CBookEntity>(20);

        private DrawerRef<string> _detailRef;

        private string searchKeyword = "";

        private string searchCatalog = "";

        private string searchAuthor = "";

        private Sortable Sortable = new Sortable();

        protected override async Task OnInitializedAsync()
        {
            Sortable.Add(nameof(CBookEntity.Title), "标题");
            Sortable.Add(nameof(CBookEntity.Author), "作者");
            Sortable.Add(nameof(CBookEntity.Path), "目录");
            Sortable.Add(nameof(CBookEntity.Name), "文件");
            Sortable.AddDesc(nameof(CBookEntity.XInsert_), "时间");

            await Search();
        }

        public async Task Search()
        {
            Console.WriteLine(" search: " + searchKeyword);

            CBookEntities = await bookService.SearchAsync(searchCatalog, searchAuthor, searchKeyword,
                Sortable.ToOrders());

            BookPaginator.DataSource = CBookEntities;

            _CBookEntities = BookPaginator.Paged().ToList();
        }

        public async Task Paging(PaginationEventArgs args)
        {
            await BookPaginator.HandlePageIndexChange(args);

            _CBookEntities = BookPaginator.Paged().ToList();
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

        public void NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }

    }
}
