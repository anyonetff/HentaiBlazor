using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class CatalogService : AbstractService
    {

        public CatalogService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BCatalogEntity>> ListAsync()
        {
            return await dbContext.BCatalogEntities.ToListAsync();
        }

        public async Task<BCatalogEntity> GetAsync(string id) 
        {
            return await dbContext.BCatalogEntities.SingleAsync(b => b.Id == id);
        }

    }
}
