using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class CatalogService : AbstractService<BCatalogEntity>
    {

        public CatalogService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

    }
}
