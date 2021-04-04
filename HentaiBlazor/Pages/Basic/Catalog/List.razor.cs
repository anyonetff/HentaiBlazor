using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class List
    {

        [Inject]
        public ModalService _modal { get; set; }

        [Inject]
        public CatalogService catalogService { get; set; }

        private ModalRef _editRef;

        private ConfirmRef _removeRef;

        private ModalRef _scanRef;

        private BCatalogEntity _catalog;


        private List<BCatalogEntity> BCatalogEntities { get; set; }

        private string searchUsage = "";

        private string searchKeyword = "";


        protected override async Task OnInitializedAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            BCatalogEntities = await catalogService.SearchAsync(searchUsage, searchKeyword);
        }

        private async Task OpenScan(string options)
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "扫描内容";
            modalConfig.Footer = null;
            modalConfig.Width = 640;
            // modalConfig.MaskClosable = false;

            modalConfig.AfterClose = async () =>
            {
                Console.WriteLine("关闭扫描对话框");

                await Search();
                StateHasChanged();
            };

            _scanRef = await _modal
                .CreateModalAsync<Scan, string>(modalConfig, options);
        }

        private async Task OpenEdit(string options)
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "编辑数据";
            modalConfig.Footer = null;

            modalConfig.AfterClose = async () =>
            {
                Console.WriteLine("AfterClose");

                await Search();

                StateHasChanged();
            };

            _editRef = await _modal
                .CreateModalAsync<Edit, string>(modalConfig, options);
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
