using AntDesign;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic.Book
{
    public partial class List
    {
        [Inject]
        public BookService service { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        List<CBookEntity> CBookEntities;
        protected override async Task OnInitializedAsync()
        {
            CBookEntities = await service.ListAsync();
        }


    }
}
