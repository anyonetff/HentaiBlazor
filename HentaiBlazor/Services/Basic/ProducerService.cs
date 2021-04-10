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
    public class ProducerService : AbstractService<BProducerEntity>
    {

        public ProducerService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<BProducerEntity> FindByNameAsync(string name)
        {
            return await this.dbContext.Set<BProducerEntity>()
                    .Where<BProducerEntity>(producer => producer.Name.Equals(name))
                    .FirstOrDefaultAsync<BProducerEntity>();
        }

        public async Task<List<BProducerEntity>> SearchAsync(string searchMode, string searchKeyword)
        {
            //var Id = new SqlParameter("producer", "%" + searchAuthor + "%");

            return await this.dbContext.Set<BProducerEntity>().AsNoTracking()
                .Where<BProducerEntity>(producer => (searchMode == "MASTER" && producer.Alias == "." && producer.Valid) || (searchMode == "ALL"))
                .Where<BProducerEntity>(producer => StringUtils.IsBlank(searchKeyword) || producer.Name.Contains(searchKeyword) || producer.Alias.Contains(searchKeyword))
                .OrderBy(producer => producer.Alias)
                .ThenBy(producer => producer.Name)
                .ToListAsync<BProducerEntity>();

        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<BProducerEntity>()
                .Where<BProducerEntity>(producer => (producer.Alias == "." && producer.Valid))
                .CountAsync<BProducerEntity>();
        }

    }
}
