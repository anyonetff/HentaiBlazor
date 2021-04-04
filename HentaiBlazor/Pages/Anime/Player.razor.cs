using HentaiBlazor.Data.Anime;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime
{
    public partial class Player
    {

        [Parameter]
        public string Id { get; set; }

        private AVideoEntity video;

        [Inject]
        public VideoService videoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("打开视频详情");

            video = await videoService.FindAsync(Id);

        }

    }
}
