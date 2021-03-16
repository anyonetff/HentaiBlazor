using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class AuthorService : AbstractService<BAuthorEntity>
    {

        public AuthorService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

    }
}
