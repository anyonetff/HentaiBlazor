using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Basic
{
    public class CatalogService : AbstractService<BCatalogEntity>
    {
        public CatalogService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BCatalogEntity>> SearchAsync(string searchUsage, string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            return await this.dbContext.Set<BCatalogEntity>()
                .Where<BCatalogEntity>(c => StringUtils.IsBlank(searchUsage) || c.Usage == searchKeyword)
                .Where<BCatalogEntity>(c => StringUtils.IsBlank(searchKeyword) || c.Path.Contains(searchKeyword))
                .OrderBy(book => book.Usage)
                .ThenBy(book => book.Path)
                .ToListAsync<BCatalogEntity>();

        }

        public async Task<List<BCatalogEntity>> ListByUsageAsync(string usage)
        {
            return await this.dbContext.Set<BCatalogEntity>()
                    .Where<BCatalogEntity>(c => (c.Usage == usage))
                    .OrderBy(c => c.Path)
                    .ToListAsync<BCatalogEntity>();
        }

        public async Task<BCatalogEntity> FindByPathAsync(string usage, string path)
        {
            return await this.dbContext.Set<BCatalogEntity>()
                    .Where<BCatalogEntity>(c => (c.Usage == usage) && (c.Path == path))
                    .FirstOrDefaultAsync<BCatalogEntity>();
        }

    }
}
