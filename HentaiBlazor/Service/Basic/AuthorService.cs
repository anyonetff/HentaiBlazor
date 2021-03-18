using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class AuthorService : AbstractService<BAuthorEntity>
    {

        public AuthorService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public BAuthorEntity FindByName(string name)
        {
            return this.dbContext.Set<BAuthorEntity>()
                    .Where<BAuthorEntity>(book => book.Name.Equals(name))
                    .FirstOrDefault<BAuthorEntity>();
        }

        public async Task<List<BAuthorEntity>> SearchAsync(string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            return await this.dbContext.Set<BAuthorEntity>()
                .Where<BAuthorEntity>(book => searchKeyword == null || searchKeyword == "" || book.Name.Contains(searchKeyword) || book.Alias.Contains(searchKeyword))
                .OrderBy(book => book.Alias)
                .OrderBy(book => book.Name)
                .ToListAsync<BAuthorEntity>();

        }

    }
}
