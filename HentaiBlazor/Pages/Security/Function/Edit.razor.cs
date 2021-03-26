using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Service.Basic;
using HentaiBlazor.Service.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security.Function
{
    public partial class Edit
    {

        private ValidationMessageStore messageStore;

        // private int _times = 0;

        private string functionId;

        private SFunctionEntity functionEntity;

        [Inject]
        public FunctionService functionService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            functionId = base.Options ?? null;

            if (StringUtils.IsBlank(functionId))
            {
                functionEntity = new SFunctionEntity { Parent = "0" };
            }
            else 
            {
                functionEntity = await functionService.FindCloneAsync(functionId);
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByParent(EditContext editContext)
        {
            if (StringUtils.IsBlank(functionEntity.Parent))
            {
                functionEntity.Parent = "0";
            }
            else if (StringUtils.IsEqual("0", functionEntity.Parent))
            {
                return;
            }

            SFunctionEntity other = await this.functionService.FindAsync(functionEntity.Parent);

            if (other == null)
            {
                messageStore.Add(editContext.Field("Parent"), "上级代码必须为[0]或未找到上级数据");
            }
        }

        private async Task OnFinish(EditContext editContext)
        {
            Console.WriteLine("开始提交表单[" + functionEntity + "].");

            messageStore = new ValidationMessageStore(editContext);

            if (editContext.GetValidationMessages().Any())
            {
                editContext.NotifyValidationStateChanged();
                messageStore.Clear();

                return;
            }

            //tagEntity.Alias = ".";

            await this.functionService.SaveAsync(functionEntity);

            await base.ModalRef.CloseAsync();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
