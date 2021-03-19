using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
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

        private ModalRef _scanRef;


        private BCatalogEntity _catalog;


        private List<BCatalogEntity> BCatalogEntities { get; set; }

        private string searchUsage;

        private string searchKeyword;

        private async Task OpenAdd()
        {
            var templateOptions = new BCatalogEntity();

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenModify(string id)
        {
            var templateOptions = await catalogService.FindAsync(id);

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenScan(string id)
        {
            var templateOptions = await catalogService.FindAsync(id);

            var modalConfig = new ModalOptions();

            modalConfig.Title = "扫描内容";
            modalConfig.Footer = null;
            modalConfig.Width = 640;

            modalConfig.AfterClose = async () =>
            {
                Console.WriteLine("AfterClose");

                await Search();
                StateHasChanged();
                // InvokeAsync(StateHasChanged);
                // return Task.CompletedTask;
            };

            _scanRef = await _modal
                .CreateModalAsync<Scan, BCatalogEntity>(modalConfig, templateOptions);
        }

        private async Task OpenEdit(BCatalogEntity templateOptions) 
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "编辑数据";
            modalConfig.Footer = null;

            modalConfig.AfterClose = async () =>
            {
                Console.WriteLine("AfterClose");

                await Search();
                StateHasChanged();
                // InvokeAsync(StateHasChanged);
                // return Task.CompletedTask;
            };

            _editRef = await _modal
                .CreateModalAsync<Edit, BCatalogEntity>(modalConfig, templateOptions);
        }


        protected override async Task OnInitializedAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            BCatalogEntities = await catalogService.SearchAsync(searchUsage, searchKeyword);
        }


        Func<ModalClosingEventArgs, Task> DeleteOnOk = (e) =>
        {
            Console.WriteLine("删除数据");

            

            return Task.CompletedTask;
        };

        Func<ModalClosingEventArgs, Task> DeleteOnCancel = (e) =>
        {
            Console.WriteLine("Cancel");
            return Task.CompletedTask;
        };

        private async Task DeleteConfirm(string Id)
        {
            _catalog = await catalogService.FindAsync(Id);

            _modal.Confirm(new ConfirmOptions()
            {
                Title = "删除数据",
                // Icon = icon,
                Content = "[" + _catalog.Usage + "] " + _catalog.Path,
                OnOk = async (r) => {
                    catalogService.Remove(_catalog);

                    await Search();
                    StateHasChanged();
                    // return Task.CompletedTask; 
                },
                OnCancel = DeleteOnCancel
            });
        }

    }
}
