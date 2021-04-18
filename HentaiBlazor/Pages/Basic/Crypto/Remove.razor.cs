using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Crypto
{
    public partial class Remove
    {
        private string cryptoId;

        private BCryptoEntity cryptoEntity;

        [Inject]
        public CryptoService cryptoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            cryptoId = base.Options;

            cryptoEntity = await cryptoService.FindAsync(cryptoId);

            await base.OnInitializedAsync();
        }

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除密码[" + cryptoEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await cryptoService.RemoveAsync(cryptoEntity);

            await base.OnFeedbackOkAsync(args);
        }
    }
}
