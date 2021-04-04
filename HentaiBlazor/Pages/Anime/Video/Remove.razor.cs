using AntDesign;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Anime;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime.Video
{
    public partial class Remove
    {
        private string videoId;

        private AVideoEntity videoEntity;

        [Inject]
        public VideoService videoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            videoId = base.Options;

            videoEntity = await videoService.FindAsync(videoId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除视频[" + videoEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await videoService.RemoveAsync(videoEntity);

            await base.OkAsync(args);
        }

    }
}
