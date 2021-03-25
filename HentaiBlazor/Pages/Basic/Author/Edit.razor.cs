using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Author
{
    public partial class Edit
    {
        private ValidationMessageStore messageStore;

        private string authorId;

        private BAuthorEntity authorEntity;

        [Inject]
        public AuthorService authorService { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            authorId = base.Options ?? null;

            if (StringUtils.IsBlank(authorId))
            {
                authorEntity = new BAuthorEntity();
            }
            else
            {
                authorEntity = (BAuthorEntity) (await authorService.FindAsync(authorId)).Clone();
                
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByAlias(EditContext editContext)
        {
            authorEntity.Alias = authorEntity.Alias.Trim();

            if (StringUtils.IsEqual(authorEntity.Alias, ".")) 
            {
                // authorEntity.Alias = ".";
                return;
            }

            if (StringUtils.IsEqual(authorEntity.Name, authorEntity.Alias))
            {
                messageStore.Add(editContext.Field("Alias"), "映射别名不能与名称相等");
                return;
            }
            
            if (StringUtils.IsNotEqual(authorEntity.Alias, "."))
            {
                BAuthorEntity other = await this.authorService.FindByNameAsync(authorEntity.Alias);

                // 如果映射的数据不存在，或不合法，或不是主数据
                if (other == null || ! other.Valid || StringUtils.IsNotEqual(other.Alias, "."))
                {
                    messageStore.Add(editContext.Field("Alias"), "映射别名未找到主数据、或匹配的主数据不合法");
                }
            }
        }

        private async Task ValidatorByName(EditContext editContext) 
        {
            BAuthorEntity other = await this.authorService.FindByNameAsync(authorEntity.Name);

            if (other != null && StringUtils.IsNotEqual(authorEntity.Id, other.Id)) 
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

                // authorEntity = await authorService.FindAsync(authorId);

                return;
            }

            await this.authorService.SaveAsync(authorEntity);

            if (authorEntity.Alias != ".")
            {
                Console.WriteLine("批量更新作者名称.");

                await this .bookService.UpdateAuthorAsync(authorEntity.Name, authorEntity.Alias);
            }

            _ = base.ModalRef.CloseAsync();

            // StateHasChanged();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine("编辑失败.");
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}
