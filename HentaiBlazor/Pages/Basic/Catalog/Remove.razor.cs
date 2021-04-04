using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Remove
    {
        private string catalogId;

        private BCatalogEntity catalogEntity;

        [Inject]
        public CatalogService catalogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            catalogId = base.Options;

            catalogEntity = await catalogService.FindAsync(catalogId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除漫画[" + catalogEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await catalogService.RemoveAsync(catalogEntity);

            await base.OkAsync(args);
        }

    }
}
