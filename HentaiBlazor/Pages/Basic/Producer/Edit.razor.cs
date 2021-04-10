using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Producer
{
    public partial class Edit
    {
        private ValidationMessageStore messageStore;

        private string producerId;

        private BProducerEntity producerEntity;

        [Inject]
        public ProducerService producerService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            producerId = base.Options ?? null;

            if (StringUtils.IsBlank(producerId))
            {
                producerEntity = new BProducerEntity();
            }
            else
            {
                producerEntity = await producerService.FindCloneAsync(producerId);
                
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByAlias(EditContext editContext)
        {
            producerEntity.Alias = producerEntity.Alias.Trim();

            if (StringUtils.IsEqual(producerEntity.Alias, ".")) 
            {
                // producerEntity.Alias = ".";
                return;
            }

            if (StringUtils.IsEqual(producerEntity.Name, producerEntity.Alias))
            {
                messageStore.Add(editContext.Field("Alias"), "映射别名不能与名称相等");
                return;
            }
            
            if (StringUtils.IsNotEqual(producerEntity.Alias, "."))
            {
                BProducerEntity other = await this.producerService.FindByNameAsync(producerEntity.Alias);

                // 如果映射的数据不存在，或不合法，或不是主数据
                if (other == null || ! other.Valid || StringUtils.IsNotEqual(other.Alias, "."))
                {
                    messageStore.Add(editContext.Field("Alias"), "映射别名未找到主数据、或匹配的主数据不合法");
                }
            }
        }

        private async Task ValidatorByName(EditContext editContext) 
        {
            BProducerEntity other = await this.producerService.FindByNameAsync(producerEntity.Name);

            if (other != null && StringUtils.IsNotEqual(producerEntity.Id, other.Id)) 
            {
                messageStore.Add(editContext.Field("Name"), "名字重复了");
            }
        }

        private async Task OnFinish(EditContext editContext)
        {
            messageStore = new ValidationMessageStore(editContext);

            await this.ValidatorByName(editContext);
            await this.ValidatorByAlias(editContext);

            if (editContext.GetValidationMessages().Any())
            {
                editContext.NotifyValidationStateChanged();
                messageStore.Clear();

                // producerEntity = await producerService.FindAsync(producerId);

                return;
            }

            await this.producerService.SaveAsync(producerEntity);

            if (producerEntity.Alias != ".")
            {
                Console.WriteLine("批量更新作者名称.");

                // await this .bookService.UpdateProducerAsync(producerEntity.Name, producerEntity.Alias);
            }

            _ = base.ModalRef.CloseAsync();

            // StateHasChanged();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }
    }
}
