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
                tagEntity = await tagService.FindAsync(tagId);
            }

            await base.OnInitializedAsync();
        }

        private async Task OnFinish(EditContext editContext)
        {
            tagEntity.Alias = ".";

            await this.tagService.SaveAsync(tagEntity);

            await base.ModalRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine("提交标签数据失败.");
        }

    }
}
