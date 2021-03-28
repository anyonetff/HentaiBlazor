using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Detail
    {
        private string bookId;

        private CBookEntity bookEntity;

        [Inject]
        public BookService bookService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bookId = base.Options;

            bookEntity = await bookService.FindCloneAsync(bookId);

            await base.OnInitializedAsync();
        }
    }
}
