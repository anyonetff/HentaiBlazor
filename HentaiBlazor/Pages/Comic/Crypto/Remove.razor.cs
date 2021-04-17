using AntDesign;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic.Crypto
{
    public partial class Remove
    {
        private string cryptoId;

        private CCryptoEntity cryptoEntity;

        [Inject]
        public CryptoService cryptoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            cryptoId = base.Options;

            cryptoEntity = await cryptoService.FindAsync(cryptoId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除密码[" + cryptoEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await cryptoService.RemoveAsync(cryptoEntity);

            await base.OkAsync(args);
        }
    }
}
