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
    public class TagService : AbstractService<BTagEntity>
    {

        public TagService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BTagEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<BTagEntity>()
                .Where<BTagEntity>(tag => StringUtils.IsBlank(searchKeyword) || tag.Name.Contains(searchKeyword))
                .OrderBy(tag => tag.Name)
                .ToListAsync<BTagEntity>();

        }

        public async Task<BTagEntity> FindByNameAsync(string name)
        {
            return await this.dbContext.Set<BTagEntity>()
                    .Where<BTagEntity>(tag => tag.Name.Equals(name))
                    .FirstOrDefaultAsync<BTagEntity>();
        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<BTagEntity>()
                .CountAsync<BTagEntity>();
        }

    }
}
