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
    }
}
