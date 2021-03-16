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
        public CatalogService service { get; set; }

        private bool _editVisible = false;

        private bool _confirmLoading = false;

        private ModalRef _editRef;

        private BCatalogEntity _catalog;


        private List<BCatalogEntity> BCatalogEntities { get; set; }

        private async Task OpenAdd()
        {
            var templateOptions = new BCatalogEntity();

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenModify(string id) 
        {
            var templateOptions = await service.FindAsync(id);

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenEdit(BCatalogEntity templateOptions) 
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "编辑数据";
            modalConfig.Footer = null;

            _editRef = await _modal
                .CreateModalAsync<Edit, BCatalogEntity>(modalConfig, templateOptions);
        }

        protected override async Task OnInitializedAsync()
        {
            BCatalogEntities = await service.ListAsync();
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
            _catalog = await service.FindAsync(Id);

            _modal.Confirm(new ConfirmOptions()
            {
                Title = "删除数据",
                // Icon = icon,
                Content = "[" + _catalog.Usage + "] " + _catalog.Path,
                OnOk = (r) => {
                    service.Remove(_catalog);

                    return Task.CompletedTask; 
                },
                OnCancel = DeleteOnCancel
            });
        }

        private void ShowModal()
        {
            _editVisible = true;
            
        }


        private async Task HandleOk(MouseEventArgs e)
        {
            _confirmLoading = true;
            StateHasChanged();
            await Task.Delay(2000);
            _editVisible = false;
            _confirmLoading = false;
        }

        private void HandleCancel(MouseEventArgs e)
        {
            Console.WriteLine("Clicked cancel button");
            _editVisible = false;
        }

    }
}
