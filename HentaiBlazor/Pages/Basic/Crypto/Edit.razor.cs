using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Crypto
{
    public partial class Edit
    {

        private ValidationMessageStore messageStore;

        // private int _times = 0;

        private string cryptoId;

        private BCryptoEntity cryptoEntity;

        [Inject]
        public CryptoService cryptoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            cryptoId = base.Options ?? null;

            if (StringUtils.IsBlank(cryptoId))
            {
                cryptoEntity = new BCryptoEntity();
            }
            else 
            {
                cryptoEntity = await cryptoService.FindCloneAsync(cryptoId);
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByName(EditContext editContext)
        {
            BCryptoEntity other = await this.cryptoService.FindBySecretAsync(cryptoEntity.Secret);

            if (other != null && StringUtils.IsNotEqual(cryptoEntity.Id, other.Id))
            {
                messageStore.Add(editContext.Field(nameof(BCryptoEntity.Secret)), "密码重复了");
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

            _ = base.FeedbackRef.CloseAsync();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
