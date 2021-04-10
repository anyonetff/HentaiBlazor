using HentaiBlazor.Common;
using HentaiBlazor.Data.Anime;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Anime;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Anime
{
    public partial class Detail
    {
        private string videoId;

        private AVideoEntity videoEntity;

        private List<AVideoTagEntity> videoTagEntities;

        private bool tagEdit = false;

        private string TagName = "";

        [Inject]
        public VideoService videoService { get; set; }

        [Inject]
        public VideoTagService videoTagService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            videoId = base.Options;

            videoEntity = await videoService.FindAsync(videoId);
            videoTagEntities = await videoTagService.ListByVideoAsync(videoId);

            await base.OnInitializedAsync();
        }

        private async Task OpenTagEdit()
        {
            Console.WriteLine("编辑标签");

            this.tagEdit = true;

            await Task.CompletedTask;
        }

        private async Task OnTagEdit()
        {
            Console.WriteLine("保存标签");

            if (TagName != null)
            {
                TagName = TagName.Trim();
            }

            if (StringUtils.IsBlank(TagName))
            {
                this.tagEdit = false;
                return;
            }

            AVideoTagEntity videoTagEntity = await this.videoTagService.FindByNameAsync(videoId, TagName);

            if (videoTagEntity == null)
            {
                videoTagEntity = new AVideoTagEntity();

                videoTagEntity.VideoId = videoId;
                videoTagEntity.TagName = TagName;

                await this.videoTagService.SaveAsync(videoTagEntity);
            }

            videoTagEntities = await videoTagService.ListByVideoAsync(videoId);

            TagName = "";

            this.tagEdit = false;
        }

        private async Task OnTagClose(string Id)
        {
            Console.WriteLine("删除标签");

            await this.videoTagService.RemoveAsync(Id);

            videoTagEntities = await videoTagService.ListByVideoAsync(videoId);
        }

        private async Task OnFavorite()
        {
            Console.WriteLine("添加/取消收藏");

            videoEntity.Favorite = !videoEntity.Favorite;

            await videoService.UpdateAsync(videoEntity);
        }

    }
}
