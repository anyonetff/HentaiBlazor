using HentaiBlazor.Data;
using HentaiBlazor.Data.Comic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Comic
{
    public class BookService : AbstractService<CBookEntity>
    {
        public BookService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

    }
}
