using HentaiBlazor.Data.Anime;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Data.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data
{
    public class HentaiContext : DbContext
    {
        public static readonly string HentaiDb = nameof(HentaiDb).ToLower();

        public DbSet<SFunctionEntity> SFunctionEntities { get; set; }
        public DbSet<SUserEntity> SUserEntities { get; set; }

        public DbSet<BProducerEntity> BProducerEntities { get; set; }
        public DbSet<BAuthorEntity> BAuthorEntities { get; set; }
        public DbSet<BTagEntity> BTagEntities { get; set; }
        public DbSet<BCatalogEntity> BCatalogEntities { get; set; }
        public DbSet<BCryptoEntity> BCryptoEntities { get; set; }

        public DbSet<CBookEntity> CBookEntities { get; set; }
        public DbSet<CBookTagEntity> CBookTagEntities { get; set; }

        public DbSet<AVideoEntity> AVideoEntities { get; set; }
        public DbSet<AVideoTagEntity> AVideoTagEntities { get; set; }

        public HentaiContext(DbContextOptions<HentaiContext> options) : base(options)
        {
            Console.WriteLine($"{ContextId} 上下文已创建.");
            Database.EnsureCreated();
        }

        public override void Dispose()
        {
            Console.WriteLine($"{ContextId} 上下文已销毁.");
            base.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            Console.WriteLine("创建数据库.");
            base.OnModelCreating(modelBuilder);
        }

        private List<BAuthorEntity> GetAuthors()
        {
            return new List<BAuthorEntity>
            {
                new BAuthorEntity { Id = "0", Name = "unknown", Alias = "."},
            };
        }

    }
}
