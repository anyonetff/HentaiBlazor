using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Tag
{
    public partial class Edit
    {

        private ValidationMessageStore messageStore;

        // private int _times = 0;

        private string tagId;

        private BTagEntity tagEntity;

        [Inject]
        public TagService tagService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tagId = base.Options ?? null;

            if (StringUtils.IsBlank(tagId))
            {
                tagEntity = new BTagEntity();
            }
            else 
            {
                tagEntity = await tagService.FindCloneAsync(tagId);
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByName(EditContext editContext)
        {
            BTagEntity other = await this.tagService.FindByNameAsync(tagEntity.Name);

            if (other != null && StringUtils.IsNotEqual(tagEntity.Id, other.Id))
            {
                // _times += 1; 跟踪同一个页面的验证次数
                // messageStore.Add(editContext.Field("Name"), "名字重复了 [第" + _times + "次验证]");
                messageStore.Add(editContext.Field("Name"), "名字重复了");
                // StateHasChanged();
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
                // Console.WriteLine("错误内容:" +  editContext.GetValidationMessages().Count());
                // 这里要用这个方面通知页面，才能触发页面渲染. 用一般的那个StateHasChanged()不管用
                editContext.NotifyValidationStateChanged();
                
                // 这里还要清空当前验证状态，不然表单永远不能触发第二次提交
                messageStore.Clear();

                return;
            }

            tagEntity.Alias = ".";

            await this.tagService.SaveAsync(tagEntity);

            await base.ModalRef.CloseAsync();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
