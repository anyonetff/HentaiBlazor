using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Services.Basic;
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

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除作者[" + authorEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await authorService.RemoveAsync(authorEntity);

            await base.OnFeedbackOkAsync(args);
        }
    }
}
