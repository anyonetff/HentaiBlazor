using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Comic;
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
                bookEntity = await bookService.FindAsync(bookId);
            }

            await base.OnInitializedAsync();
        }

        private void OnFinish(EditContext editContext)
        {


            _ = base.DrawerRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }

    }
}
