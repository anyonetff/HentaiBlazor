using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Producer
{
    public partial class Remove
    {
        private string tagId;

        private BProducerEntity producerEntity;

        [Inject]
        public ProducerService producerService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tagId = base.Options;

            producerEntity = await producerService.FindAsync(tagId);

            await base.OnInitializedAsync();
        }

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除制作公司[" + producerEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await producerService.RemoveAsync(producerEntity);

            await base.OnFeedbackOkAsync(args);
        }
    }
}
