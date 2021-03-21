using HentaiBlazor.Data;
using HentaiBlazor.Data.Anime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Anime
{
    public class VideoService : AbstractService<AVideoEntity>
    {

        public VideoService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<AVideoEntity>()
                .CountAsync<AVideoEntity>();
        }

    }
}
