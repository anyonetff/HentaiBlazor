using AntDesign;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic.Book
{
    public partial class Remove
    {
        private string bookId;

        private CBookEntity bookEntity;

        [Inject]
        public BookService bookService { get; set; }

        private bool eliminate;

        protected override async Task OnInitializedAsync()
        {
            bookId = base.Options;

            bookEntity = await bookService.FindAsync(bookId);

            await base.OnInitializedAsync();
        }

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除漫画[" + bookEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await bookService.RemoveAsync(bookEntity);

            await base.OnFeedbackOkAsync(args);
        }

    }
}
