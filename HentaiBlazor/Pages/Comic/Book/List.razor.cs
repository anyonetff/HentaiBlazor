using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
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
        public DrawerService _drawer { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        private DrawerRef<string> _editRef;

        private ConfirmRef _removeRef;

        private List<CBookEntity> CBookEntities;

        private string searchKeyword;

        private string searchCatalog;

        private string searchAuthor;

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
            Console.WriteLine("搜索数据.");

            CBookEntities = await bookService.SearchAsync(searchCatalog, searchAuthor, searchKeyword,
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

        private async Task OpenDuplicate(string options)
        {
            var _config = new DrawerOptions();

            _config.Title = "重复作品";
            _config.Width = 800;
            //modalConfig.Footer = null;

            _editRef = await _drawer
                .CreateAsync<Duplicate, string, string>(_config, options);

            _editRef.OnClosed = async (r) =>
            {
                Console.WriteLine(r);

                await Search();
                StateHasChanged();
            };
        }

    }
}
