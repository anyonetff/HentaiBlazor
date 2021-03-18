using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages
{
    public partial class Welcome
    {

        [Inject]
        public BookService bookService { get; set; }

        private int BookTotal;

        private int AnimeTotal;

        protected override async Task OnInitializedAsync()
        {

            BookTotal = await bookService.TotalCountAsync();
        }

    }
}
