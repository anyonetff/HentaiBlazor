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

        private CBookEntity book;

        [Inject]
        public BookService bookService { get; set; }

        protected override void OnInitialized()
        {
            //catalog = new BCatalogEntity();
            book = base.Options ?? new CBookEntity();
            base.OnInitialized();
        }

        private void OnFinish(EditContext editContext)
        {
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(_model)}");


            if (book.Id == null || book.Id == "")
            {
            }
            else
            {
            }

            _ = base.ModalRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }

    }
}
