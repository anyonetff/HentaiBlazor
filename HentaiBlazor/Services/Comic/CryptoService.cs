using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Comic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Comic
{
    public class CryptoService : AbstractService<CCryptoEntity>
    {
        public CryptoService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<CCryptoEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<CCryptoEntity>()
                .Where<CCryptoEntity>(crypto => StringUtils.IsBlank(searchKeyword) || crypto.Secret.Contains(searchKeyword))
                .OrderBy(crypto => crypto.Secret)
                .ToListAsync<CCryptoEntity>();
        }

        public async Task<CCryptoEntity> FindBySecretAsync(string secret)
        {
            return await this.dbContext.Set<CCryptoEntity>()
                    .Where<CCryptoEntity>(crypto => crypto.Secret.Equals(secret))
                    .FirstOrDefaultAsync<CCryptoEntity>();
        }
    }
}
