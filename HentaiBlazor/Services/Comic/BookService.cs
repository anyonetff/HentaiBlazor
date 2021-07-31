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

        public async Task<PagedList<CBookEntity>> SearchPagedAsync(
            string searchPath, string searchAuthor, string searchKeyword, IEnumerable<SortableItem> sortables,
            int pageSize, int pageIndex)
        {
            var query = this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchPath) || book.Path.Contains(searchPath))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchAuthor) || book.Author.Contains(searchAuthor))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchKeyword) || book.Title.Contains(searchKeyword) || book.Name.Contains(searchKeyword));

            query = OrderUtils.Sort<CBookEntity>(query, sortables);

            int totalCount = query.Count();

            PagedList<CBookEntity> result = new PagedList<CBookEntity>(pageSize, pageIndex, totalCount);

            result.Data = await query
                .Skip(result.Skip())
                .Take(result.Take())
                .ToListAsync<CBookEntity>();

            return result;
        }

        public async Task<List<CBookEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword, IEnumerable<SortableItem> sortables)
        {
            var query = this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchPath) || book.Path.Contains(searchPath))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchAuthor) || book.Author.Contains(searchAuthor))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchKeyword) || book.Title.Contains(searchKeyword) || book.Name.Contains(searchKeyword));

            query = OrderUtils.Sort<CBookEntity>(query, sortables);

            return await query.ToListAsync<CBookEntity>();
        }

        public async Task<CBookEntity> FindByNameAsync(string path, string name)
        {
            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => book.Path.Equals(path) && book.Name.Equals(name))
                .FirstOrDefaultAsync<CBookEntity>();
        }

        public async Task<List<CBookEntity>> ListByAuthorAsync(string author)
        {
            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => book.Author == author)
                .OrderBy(book => book.Title)
                .ToListAsync<CBookEntity>();
        }

        public async Task<List<CBookEntity>> ListByAuthorAsync(List<string> authors)
        {
            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => authors.Contains(book.Author))
                .OrderBy(book => book.Title)
                .ToListAsync<CBookEntity>();
        }


        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.Set<CBookEntity>()
                .CountAsync<CBookEntity>();
        }

        public async Task<int> TotalCountAsync(string path, bool children)
        {
            return await this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => children ? book.Path.StartsWith(path) : book.Path.Equals(path))
                .CountAsync<CBookEntity>();
        }

        public async Task<int> UpdateAuthorAsync(string author, string alias)
        {
            await this.dbContext.Set<CBookEntity>()
                .Where(book => book.Author == author)
                .ForEachAsync(book => book.Author = alias);

            return await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<(string, int)>> DuplicateNameAsync()
        {
            return await this.dbContext.Set<CBookEntity>()
                .GroupBy(b => new { b.Name })
                .Select(b => new ValueTuple<string, int>(b.Key.Name, b.Count()))
                .ToListAsync<ValueTuple<string, int>>();
        }


    }
}
