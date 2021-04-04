﻿using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Anime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Anime
{
    public class VideoService : AbstractService<AVideoEntity>
    {

        public VideoService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<AVideoEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            var query = this.dbContext.Set<AVideoEntity>()
                .Where<AVideoEntity>(video => StringUtils.IsBlank(searchPath) || video.Path.Contains(searchPath))
                .Where<AVideoEntity>(video => StringUtils.IsBlank(searchAuthor) || video.Author.Contains(searchAuthor))
                .Where<AVideoEntity>(video => StringUtils.IsBlank(searchKeyword) || video.Title.Contains(searchKeyword) || video.Name.Contains(searchKeyword))
                .OrderBy(video => video.Author).ThenBy(video => video.Title);

            return await query.ToListAsync<AVideoEntity>();

        }

        public async Task<AVideoEntity> FindByNameAsync(string path, string name)
        {
            return await this.dbContext.Set<AVideoEntity>()
                .Where<AVideoEntity>(book => book.Path.Equals(path) && book.Name.Equals(name))
                .FirstOrDefaultAsync<AVideoEntity>();
        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<AVideoEntity>()
                .CountAsync<AVideoEntity>();
        }


    }
}