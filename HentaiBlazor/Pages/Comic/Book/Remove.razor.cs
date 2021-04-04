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

        protected override async Task OnInitializedAsync()
        {
            bookId = base.Options;

            bookEntity = await bookService.FindAsync(bookId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除漫画[" + bookEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await bookService.RemoveAsync(bookEntity);

            await base.OkAsync(args);
        }

    }
}
