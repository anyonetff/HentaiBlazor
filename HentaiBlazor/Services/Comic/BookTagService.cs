using HentaiBlazor.Data;
using HentaiBlazor.Data.Comic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Comic
{
    public class BookTagService : AbstractService<CBookTagEntity>
    {
        public BookTagService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<CBookTagEntity>> ListByBookAsync(string bookId)
        {
            return await this.dbContext.Set<CBookTagEntity>()
                .Where<CBookTagEntity>(bt => bt.BookId == bookId)
                .OrderBy(bt => bt.TagName)
                .ToListAsync<CBookTagEntity>();
        }

        public async Task<CBookTagEntity> FindByNameAsync(string bookId, string tagName)
        {
            return await this.dbContext.Set<CBookTagEntity>()
                .Where<CBookTagEntity>(bt => bt.BookId == bookId && bt.TagName == tagName)
                .FirstOrDefaultAsync<CBookTagEntity>();
        }

    }
}
