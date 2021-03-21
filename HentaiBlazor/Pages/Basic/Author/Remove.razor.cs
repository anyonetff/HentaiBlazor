using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Author
{
    public partial class Remove
    {
        private string tagId;

        private BAuthorEntity authorEntity;

        [Inject]
        public AuthorService authorService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tagId = base.Options;

            authorEntity = await authorService.FindAsync(tagId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除标签[" + authorEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await authorService.RemoveAsync(authorEntity);

            await base.OkAsync(args);
        }
    }
}
