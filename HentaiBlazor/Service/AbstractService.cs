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
            return await this.dbContext.Set<T>()
                .OrderBy(entity => entity.Id)
                .ToListAsync<T>();
        }

        public async Task<T> FindAsync(string id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public T Find(string id) 
        {
            return this.dbContext.Set<T>().Find(id);
        }
        
        // 保存一个实体.
        // 如果实体不存在则新建，存在则更新.
        // 实体如果没有对主键赋值，则使用UUID生成主键.
        public T Save(T entity)
        {
            if (entity.Id == null || entity.Id == "")
            {
                entity.Id = Guid.NewGuid().ToString();

                this.Add(entity);
                return entity;
            }

            T other = dbContext.Set<T>().Find(entity.Id);

            if (other == null)
            {
                this.Add(entity);
                return entity;
            }

            this.Update(entity);

            return entity;
        }

        // 添加一个实体.
        public int Add(T entity)
        {
            entity.XInsert_ = DateTime.Now;
            entity.XUpdate_ = DateTime.Now;

            dbContext.Set<T>().Add(entity);
            
            return dbContext.SaveChanges();
        }

        // 修改一个实体
        public int Update(T entity)
        {
            entity.XUpdate_ = DateTime.Now;

            dbContext.Set<T>().Update(entity);

            return dbContext.SaveChanges();
        }

        // 删除一个实体
        public int Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

    }
}
