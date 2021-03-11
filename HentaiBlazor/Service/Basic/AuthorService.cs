using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class AuthorService : AbstractService
    {

        public AuthorService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BAuthorEntity>> ListAsync()
        {
            return await dbContext.BAuthorEntities.ToListAsync();
        }
    }
}
