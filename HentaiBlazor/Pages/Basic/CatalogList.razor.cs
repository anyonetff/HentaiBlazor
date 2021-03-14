using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic
{
    public partial class CatalogList
    {
        [Inject]
        public CatalogService service { get; set; }

        private List<BCatalogEntity> BCatalogEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BCatalogEntities = await service.ListAsync();
        }

    }
}
