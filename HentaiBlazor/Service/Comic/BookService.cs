using HentaiBlazor.Data;
using HentaiBlazor.Data.Comic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Comic
{
    public class BookService : AbstractService<CBookEntity>
    {
        public BookService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<CBookEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>( book => book.Author.Contains(searchAuthor) )
                
                .ToListAsync<CBookEntity>();

        }

    }
}
