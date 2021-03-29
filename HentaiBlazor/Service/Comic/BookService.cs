using HentaiBlazor.Common;
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

        public async Task<List<CBookEntity>> SearchAsync(string searchPath, string searchAuthor, string searchKeyword)
        {
            //var Id = new SqlParameter("author", "%" + searchAuthor + "%");

            var query = this.dbContext.Set<CBookEntity>()
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchPath) || book.Path.Contains(searchPath))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchAuthor) || book.Author.Contains(searchAuthor))
                .Where<CBookEntity>(book => StringUtils.IsBlank(searchKeyword) || book.Title.Contains(searchKeyword) || book.Name.Contains(searchKeyword));

            query.OrderBy(book => book.Author).ThenBy(book => book.Title);

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
