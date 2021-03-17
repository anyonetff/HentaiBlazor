using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
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
        private BAuthorEntity author;

        [Inject]
        public AuthorService authorService { get; set; }

        protected override void OnInitialized()
        {
            //catalog = new BCatalogEntity();
            author = base.Options ?? new BAuthorEntity();
            base.OnInitialized();
        }

        private void OnFinish(EditContext editContext)
        {
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(_model)}");

            if (author.Name == author.Alias)
            {
                author.Alias = ".";
            }

            if (author.Id == null || author.Id == "")
            {
                Console.WriteLine("添加一条记录");
                author.Id = Guid.NewGuid().ToString();

                authorService.Add(author);
            }
            else
            {
                Console.WriteLine("修改一条记录");
                authorService.Update(author);
            }

            _ = base.ModalRef.CloseAsync();

            StateHasChanged();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}
