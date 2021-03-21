using AntDesign;
using HentaiBlazor.Common;
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

        private string catalogId;

        private BCatalogEntity catalogEntity;

        [Inject]
        public CatalogService catalogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            catalogId = base.Options ?? null;

            if (StringUtils.IsBlank(catalogId))
            {
                catalogEntity = new BCatalogEntity();
            }
            else
            {
                catalogEntity = await catalogService.FindAsync(catalogId);
            }

            await base.OnInitializedAsync();
        }

        private async Task OnFinish(EditContext editContext)
        {
            await this.catalogService.SaveAsync(catalogEntity);

            await base.ModalRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}
