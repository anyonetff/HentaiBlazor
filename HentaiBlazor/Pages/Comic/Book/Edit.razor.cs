using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic.Book
{
    public partial class Edit
    {

        private string bookId;

        private CBookEntity bookEntity;

        [Inject]
        public BookService bookService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bookId = base.Options ?? null;

            if (StringUtils.IsBlank(bookId))
            {
                bookEntity = new CBookEntity();
            }
            else
            {
                bookEntity = await bookService.FindCloneAsync(bookId);
            }

            await base.OnInitializedAsync();
        }

        private async Task OnFinish(EditContext editContext)
        {
            await bookService.SaveAsync(bookEntity);

            await this.FeedbackRef.CloseAsync();
            // await ((DrawerRef<string>)base.FeedbackRef)?.CloseAsync("success");
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
