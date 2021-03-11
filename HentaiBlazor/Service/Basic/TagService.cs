﻿using HentaiBlazor.Data;
using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service.Basic
{
    public class TagService : AbstractService
    {

        public TagService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<BTagEntity>> ListAsync()
        {
            return await dbContext.BTagEntities.ToListAsync();
        }
    }
}
