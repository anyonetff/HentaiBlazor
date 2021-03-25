using HentaiBlazor.Common;
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

        public async Task<BAuthorEntity> FindByNameAsync(string name)
        {
            return await this.dbContext.Set<BAuthorEntity>()
                    .Where<BAuthorEntity>(book => book.Name.Equals(name))
                    .FirstOrDefaultAsync<BAuthorEntity>();
        }


        public async Task<List<BAuthorEntity>> SearchAsync(string searchMode, string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            return await this.dbContext.Set<BAuthorEntity>().AsNoTracking()
                .Where<BAuthorEntity>(author => (searchMode == "MASTER" && author.Alias == "." && author.Valid) || (searchMode == "ALL"))
                .Where<BAuthorEntity>(author => StringUtils.IsBlank(searchKeyword) || author.Name.Contains(searchKeyword) || author.Alias.Contains(searchKeyword))
                .OrderBy(author => author.Alias)
                .ThenBy(author => author.Name)
                .ToListAsync<BAuthorEntity>();

        }

    }
}
