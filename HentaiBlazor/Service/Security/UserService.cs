using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Security
{
    public class UserService : AbstractService<SUserEntity>
    {
        public UserService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<SUserEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<SUserEntity>()
                .Where<SUserEntity>(f => StringUtils.IsBlank(searchKeyword) || f.Name.Contains(searchKeyword) || f.Username.Contains(searchKeyword))
                .OrderBy(f => f.Username)
                .ToListAsync<SUserEntity>();
        }

    }
}
