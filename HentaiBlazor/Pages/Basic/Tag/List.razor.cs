using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Tag
{
    public partial class List
    {
        [Inject]
        public TagService service { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        List<BTagEntity> BTagEntities;
        protected override async Task OnInitializedAsync()
        {
            BTagEntities = await service.ListAsync();
        }


    }
}
