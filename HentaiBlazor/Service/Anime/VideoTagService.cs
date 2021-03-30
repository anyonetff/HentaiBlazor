using HentaiBlazor.Data;
using HentaiBlazor.Data.Anime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Comic
{
    public class VideoTagService : AbstractService<AVideoTagEntity>
    {
        public VideoTagService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<AVideoTagEntity>> ListByVideoAsync(string videoId)
        {
            return await this.dbContext.Set<AVideoTagEntity>()
                .Where<AVideoTagEntity>(bt => bt.VideoId == videoId)
                .OrderBy(bt => bt.TagName)
                .ToListAsync<AVideoTagEntity>();
        }

        public async Task<AVideoTagEntity> FindByNameAsync(string videoId, string tagName)
        {
            return await this.dbContext.Set<AVideoTagEntity>()
                .Where<AVideoTagEntity>(bt => bt.VideoId == videoId && bt.TagName == tagName)
                .FirstOrDefaultAsync<AVideoTagEntity>();
        }

    }
}
