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
    public class CryptoService : AbstractService<BCryptoEntity>
    {
        public CryptoService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BCryptoEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<BCryptoEntity>()
                .Where<BCryptoEntity>(crypto => StringUtils.IsBlank(searchKeyword) || crypto.Secret.Contains(searchKeyword))
                .OrderBy(crypto => crypto.Secret)
                .ToListAsync<BCryptoEntity>();
        }

        public async Task<BCryptoEntity> FindBySecretAsync(string secret)
        {
            return await this.dbContext.Set<BCryptoEntity>()
                    .Where<BCryptoEntity>(crypto => crypto.Secret.Equals(secret))
                    .FirstOrDefaultAsync<BCryptoEntity>();
        }
    }
}
