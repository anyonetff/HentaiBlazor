using HentaiBlazor.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Service
{
    /**
     * 增删改查服务基类.
     * 
     */
    public abstract class AbstractService<T> where T : AbstractEntity
    {
        protected HentaiContext dbContext;

        public AbstractService(IDbContextFactory<HentaiContext> dbFactory)
        {
            this.dbContext = dbFactory.CreateDbContext();
        }

        public async Task<List<T>> ListAsync()
        {
            return await this.dbContext.Set<T>().ToListAsync<T>();
        }

        public async Task<T> FindAsync(string id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public int Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChanges();
        }

        public int Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

    }
}
