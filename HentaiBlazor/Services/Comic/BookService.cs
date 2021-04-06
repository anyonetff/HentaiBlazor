using HentaiBlazor.Common;
using HentaiBlazor.Data;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HentaiBlazor.Services.Comic
{
    public class BookService : AbstractService<CBookEntity>
    {
        public BookService(IDbContextFactory<HentaiContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<CBookEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword, IEnumerable<Sortable> sortables)
        {
            var query = this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchPath) || book.Path.Contains(searchPath))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchAuthor) || book.Author.Contains(searchAuthor))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchKeyword) || book.Title.Contains(searchKeyword) || book.Name.Contains(searchKeyword));

            var _query = OrderUtils.Sort<CBookEntity>(query, sortables);

            if (_query == null)
            {
                Console.WriteLine("没有任何排序.");
                return await query.ToListAsync<CBookEntity>();
            }

            return await _query.ToListAsync<CBookEntity>();
        }

        private Expression<Func<CBookEntity, TResult>> GetExpression<TResult>(string name)
        {

            return null;
        }


        public async Task<List<CBookEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword, Sortable sortable)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            var query = this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchPath) || book.Path.Contains(searchPath))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchAuthor) || book.Author.Contains(searchAuthor))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchKeyword) || book.Title.Contains(searchKeyword) || book.Name.Contains(searchKeyword));

            switch (sortable.Name)
            {
                case nameof(CBookEntity.Author):
                    query = sortable.Mode > 0 ? 
                        query.OrderBy(book => book.Author).ThenBy(book => book.Title) : 
                        query.OrderByDescending(book => book.Author).ThenByDescending(book => book.Title);
                    break;
                case nameof(CBookEntity.Name):
                    query = sortable.Mode > 0 ? 
                        query.OrderBy(book => book.Name).ThenBy(book => book.Path) : 
                        query.OrderByDescending(book => book.Name).ThenByDescending(book => book.Path);
                    break;
                default:
                    query = sortable.Mode > 0 ? 
                        query.OrderBy(book => book.XInsert_) : 
                        query.OrderByDescending(book => book.XInsert_);
                    break;
            }

            return await query.ToListAsync<CBookEntity>();

        }

        public async Task<CBookEntity> FindByNameAsync(string path, string name)
        {
            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => book.Path.Equals(path) && book.Name.Equals(name))
                .FirstOrDefaultAsync<CBookEntity>();
        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<CBookEntity>()
                .CountAsync<CBookEntity>();
        }

        public async Task<int> UpdateAuthorAsync(string author, string alias)
        {
            await this.dbContext.Set<CBookEntity>()
                .Where(book => book.Author == author)
                .ForEachAsync(book => book.Author = alias);

            return await this.dbContext.SaveChangesAsync();
        }


    }
}
