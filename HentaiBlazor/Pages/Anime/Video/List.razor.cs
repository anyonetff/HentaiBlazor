using AntDesign;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Service.Anime;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime.Video
{
    public partial class List
    {
        [Inject]
        public VideoService service { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        List<AVideoEntity> AVideoEntities;
        protected override async Task OnInitializedAsync()
        {
            AVideoEntities = await service.ListAsync();
        }


    }
}
