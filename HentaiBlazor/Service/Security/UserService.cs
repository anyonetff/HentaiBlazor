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

    }
}
