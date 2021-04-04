using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Security
{
    public class UserService : AbstractService<SUserEntity>
    {
        public UserService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<SUserEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<SUserEntity>()
                .Where<SUserEntity>(u => StringUtils.IsBlank(searchKeyword) || u.Name.Contains(searchKeyword) || u.Username.Contains(searchKeyword))
                .OrderBy(u => u.Username)
                .ToListAsync<SUserEntity>();
        }

        public async Task<SUserEntity> FindByUsernameAsync(string username)
        {
            return await this.dbContext.Set<SUserEntity>()
                .Where<SUserEntity>(u => u.Username == username)
                .FirstOrDefaultAsync<SUserEntity>();
        }

    }
}
