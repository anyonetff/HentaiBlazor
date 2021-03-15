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
        public CatalogService service { get; set; }

        private bool _editVisible = false;

        private bool _confirmLoading = false;

        private ModalRef _editRef;

        private BCatalogEntity _catalog;


        private List<BCatalogEntity> BCatalogEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BCatalogEntities = await service.ListAsync();
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
