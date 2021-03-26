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
    public class FunctionService : AbstractService<SFunctionEntity>
    {
        public FunctionService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<SFunctionEntity>> SearchAsync(string searchKeyword)
        {
            return await this.dbContext.Set<SFunctionEntity>()
                .Where<SFunctionEntity>(f => StringUtils.IsBlank(searchKeyword) || f.Name.Contains(searchKeyword))
                .OrderBy(f => f.Id)
                .ToListAsync<SFunctionEntity>();
        }

    }
}
