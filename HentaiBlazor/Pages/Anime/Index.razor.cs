using AntDesign;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime
{
    public partial class Index
    {


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public VideoService videoService { get; set; }

        [Inject]
        public DrawerService _drawer { get; set; }

        private List<AVideoEntity> AVideoEntities;

        private List<AVideoEntity> _AVideoEntities;

        private Paginator<AVideoEntity> VideoPaginator = new Paginator<AVideoEntity>(10);

        private DrawerRef<string> _detailRef;

        private string searchKeyword = "";

        private string searchCatalog = "";

        private string searchAuthor = "";

        private Sortable Sortable = new Sortable();

        protected override async Task OnInitializedAsync()
        {
            Sortable.Add(nameof(AVideoEntity.Title), "标题");
            Sortable.Add(nameof(AVideoEntity.Producer), "作者");
            Sortable.Add(nameof(AVideoEntity.Path), "目录");
            Sortable.Add(nameof(AVideoEntity.Name), "文件");
            Sortable.AddDesc(nameof(AVideoEntity.XInsert_), "时间");

            await Search();
        }

        public async Task Search()
        {
            Console.WriteLine(" search: " + searchKeyword);

            AVideoEntities = await videoService.SearchAsync(searchCatalog, searchAuthor, searchKeyword,
                Sortable.ToOrders());

            VideoPaginator.DataSource = AVideoEntities;

            _AVideoEntities = VideoPaginator.Paged().ToList();
        }

        public async Task _paging(PaginationEventArgs args)
        {
            await VideoPaginator.HandlePageIndexChange(args);

            _AVideoEntities = VideoPaginator.Paged().ToList();
        }

        public async Task _sizing(PaginationEventArgs args)
        {
            await VideoPaginator.HandlePageSizeChange(args);

            _AVideoEntities = VideoPaginator.Paged().ToList();
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
