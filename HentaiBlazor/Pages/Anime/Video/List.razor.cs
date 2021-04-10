using AntDesign;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime.Video
{
    public partial class List
    {
        [Inject]
        public VideoService videoService { get; set; }

        [Inject]
        public DrawerService _drawer { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        private DrawerRef<string> _editRef;

        private ConfirmRef _removeRef;

        private List<AVideoEntity> AVideoEntities;

        private string searchKeyword;

        private string searchCatalog;

        private string searchAuthor;

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
            AVideoEntities = await videoService.SearchAsync(searchCatalog, searchAuthor, searchKeyword,
                Sortable.ToOrders());
        }

        private async Task OpenEdit(string options)
        {
            var _config = new DrawerOptions();

            _config.Title = "编辑数据";
            _config.Width = 800;
            //modalConfig.Footer = null;

            _editRef = await _drawer
                .CreateAsync<Edit, string, string>(_config, options);

            _editRef.OnClosed = async (r) =>
            {
                Console.WriteLine(r);

                await Search();
                StateHasChanged();
            };
        }

        private async Task OpenRemove(string options)
        {
            var _config = new ConfirmOptions();

            _config.Title = "删除数据";

            _config.OnOk = async (r) =>
            {
                Console.WriteLine("关闭删除确认后刷新数据...");

                await Search();
                StateHasChanged();
            };

            _removeRef = await _modal.CreateConfirmAsync<Remove, string, string>(_config, options);
        }
    }
}
