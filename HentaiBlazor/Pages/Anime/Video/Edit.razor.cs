using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Services.Anime;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime.Video
{
    public partial class Edit
    {

        private string videoId;

        private AVideoEntity videoEntity;

        [Inject]
        public VideoService videoService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            videoId = base.Options ?? null;

            if (StringUtils.IsBlank(videoId))
            {
                videoEntity = new AVideoEntity();
            }
            else
            {
                videoEntity = await videoService.FindCloneAsync(videoId);
            }

            await base.OnInitializedAsync();
        }

        private async Task OnFinish(EditContext editContext)
        {
            await videoService.SaveAsync(videoEntity);

            await this.FeedbackRef.CloseAsync();
            // await ((DrawerRef<string>)base.FeedbackRef)?.CloseAsync("success");
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}
