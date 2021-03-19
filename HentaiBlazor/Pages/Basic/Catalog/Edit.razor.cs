using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Edit
    {

        private BCatalogEntity catalog;

        [Inject]
        public CatalogService catalogService { get; set; }

        protected override void OnInitialized()
        {
            //catalog = new BCatalogEntity();
            catalog = base.Options ?? new BCatalogEntity();
            base.OnInitialized();
        }

        private void OnFinish(EditContext editContext)
        {
            catalogService.Save(catalog);

            // StateHasChanged();

            _ = base.ModalRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}
