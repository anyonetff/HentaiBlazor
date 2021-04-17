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

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除档案目录[" + catalogEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await catalogService.RemoveAsync(catalogEntity);

            await base.OnFeedbackOkAsync(args);
        }

    }
}
