using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic.Crypto
{
    public partial class Edit
    {

        private ValidationMessageStore messageStore;

        // private int _times = 0;

        private string cryptoId;

        private CCryptoEntity cryptoEntity;

        [Inject]
        public CryptoService cryptoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            cryptoId = base.Options ?? null;

            if (StringUtils.IsBlank(cryptoId))
            {
                cryptoEntity = new CCryptoEntity();
            }
            else 
            {
                cryptoEntity = await cryptoService.FindCloneAsync(cryptoId);
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByName(EditContext editContext)
        {
            CCryptoEntity other = await this.cryptoService.FindBySecretAsync(cryptoEntity.Secret);

            if (other != null && StringUtils.IsNotEqual(cryptoEntity.Id, other.Id))
            {
                messageStore.Add(editContext.Field(nameof(CCryptoEntity.Secret)), "密码重复了");
            }
        }
        
        private async Task OnFinish(EditContext editContext)
        {
            Console.WriteLine("开始提交表单.");

            // 拿到当前表单的验证消息
            messageStore = new ValidationMessageStore(editContext);

            await this.ValidatorByName(editContext);

            if (editContext.GetValidationMessages().Any())
            {
                editContext.NotifyValidationStateChanged();
                
                messageStore.Clear();

                return;
            }

            await this.cryptoService.SaveAsync(cryptoEntity);

            await base.ModalRef.CloseAsync();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
