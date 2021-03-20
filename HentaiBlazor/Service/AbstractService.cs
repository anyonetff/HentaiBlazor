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

        public List<T> List()
        {
            return this.dbContext.Set<T>()
                .OrderBy(entity => entity.Id)
                .ToList<T>();
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
                Console.WriteLine("生成ID并添加一条数据.");

                entity.Id = Guid.NewGuid().ToString();

                this.Add(entity);
                return entity;
            }

            T other = dbContext.Set<T>().Find(entity.Id);

            if (other == null)
            {
                Console.WriteLine("使用现有ID添加一条数据.");

                this.Add(entity);
                return entity;
            }

            this.Update(entity);

            return entity;
        }

        public async Task<T> SaveAsync(T entity)
        {
            if (entity.Id == null || entity.Id == "")
            {
                Console.WriteLine("生成ID并添加一条数据.");

                entity.Id = Guid.NewGuid().ToString();

                await this.AddAsync(entity);

                return entity;
            }

            T other = await dbContext.Set<T>().FindAsync(entity.Id);

            if (other == null)
            {
                Console.WriteLine("使用现有ID添加一条数据.");

                await this.AddAsync(entity);

                return entity;
            }

            await this.UpdateAsync(entity);

            return entity;
        }

        // 添加一个实体.
        public int Add(T entity)
        {
            Console.WriteLine("添加一条新数据[" + entity.Id + "]");

            // entity.XInsert_ = DateTime.Now;
            // entity.XUpdate_ = DateTime.Now;

            dbContext.Set<T>().Add(entity);
            
            return dbContext.SaveChanges();
        }

        public async Task<int> AddAsync(T entity)
        {
            Console.WriteLine("添加一条新数据[" + entity.Id + "]");

            // entity.XInsert_ = DateTime.Now;
            // entity.XUpdate_ = DateTime.Now;

            await dbContext.Set<T>().AddAsync(entity);

            return await dbContext.SaveChangesAsync();
        }


        // 修改一个实体
        public int Update(T entity)
        {
            entity.XUpdate_ = DateTime.Now;

            dbContext.Set<T>().Update(entity);

            return dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            entity.XUpdate_ = DateTime.Now;

            dbContext.Set<T>().Update(entity);

            return await dbContext.SaveChangesAsync();
        }

        // 删除一个实体
        public int Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);

            return dbContext.SaveChanges();
        }

        public async Task<int> RemoveAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);

            return await dbContext.SaveChangesAsync();
        }

    }
}
