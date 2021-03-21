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
        //private CustomValidator customValidator;

        private BAuthorEntity author;

        [Inject]
        public AuthorService authorService { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        protected override void OnInitialized()
        {
            //catalog = new BCatalogEntity();
            author = base.Options ?? new BAuthorEntity();
            base.OnInitialized();
        }

        private async Task<bool> validatorByAlias(BAuthorEntity author)
        {
            author.Alias = author.Alias.Trim();

            if (StringUtils.IsBlank(author.Alias) || author.Name == author.Alias)
            {
                author.Alias = ".";

                return false;
            }
            
            if (author.Alias != ".")
            {
                BAuthorEntity other = await this.authorService.FindByNameAsync(author.Alias);

                // 如果映射的数据不存在，或不合法
                if (other == null || ! other.Valid)
                {
                    author.Alias = ".";
                    
                    return false;
                }

                if (other.Alias != ".")
                {
                    author.Alias = other.Alias;

                    return false;
                }
            }

            return true;
        }

        private async Task<bool> validatorByName(BAuthorEntity author) 
        {
            BAuthorEntity other = await this.authorService.FindByNameAsync(author.Name);

            if (other != null && StringUtils.IsNotEqual(author.Id, other.Id, StringComparison.OrdinalIgnoreCase)) 
            {
                return false;
            }

            return true;
        }

        private async Task OnFinish(EditContext editContext)
        {
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(_model)}");

            if (! await this.validatorByName(author))
            {
                return;
            }

            await this.validatorByAlias(author);


            await this.authorService.SaveAsync(author);

            if (author.Alias != ".")
            {
                Console.WriteLine("批量更新作者名称.");

                await this .bookService.UpdateAuthorAsync(author.Name, author.Alias);
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
