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
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        public CBookEntity book;

        protected override async Task OnInitializedAsync()
        {
            // _model.Pagination.Pagination.PageSize = 16;

            book = await bookService.FindAsync(Id);
        }

    }
}
