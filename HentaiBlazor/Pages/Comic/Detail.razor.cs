using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Detail
    {
        private string bookId;

        private CBookEntity bookEntity;

        private List<CBookTagEntity> bookTagEntities;

        private bool tagEdit = false;

        private string TagName = "";

        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public BookTagService bookTagService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bookId = base.Options;

            bookEntity = await bookService.FindAsync(bookId);
            bookTagEntities = await bookTagService.ListByBookAsync(bookId);

            await base.OnInitializedAsync();
        }

        private async Task OpenTagEdit()
        {
            Console.WriteLine("编辑标签");

            this.tagEdit = true;

            await Task.CompletedTask;
        }

        private async Task OnTagEdit()
        {
            Console.WriteLine("保存标签");

            if (TagName != null)
            {
                TagName = TagName.Trim();
            }

            if (StringUtils.IsBlank(TagName))
            {
                this.tagEdit = false;
                return;
            }

            CBookTagEntity bookTagEntity = await this.bookTagService.FindByNameAsync(bookId, TagName);

            if (bookTagEntity == null)
            {
                bookTagEntity = new CBookTagEntity();

                bookTagEntity.BookId = bookId;
                bookTagEntity.TagName = TagName;

                await this.bookTagService.SaveAsync(bookTagEntity);
            }

            bookTagEntities = await bookTagService.ListByBookAsync(bookId);

            TagName = "";

            this.tagEdit = false;
        }

        private async Task OnTagClose(string Id)
        {
            CBookTagEntity bookTagEntity = await this.bookTagService.FindAsync(Id);

            if (bookTagEntity != null)
            {
                await this.bookTagService.RemoveAsync(bookTagEntity);
            }

            bookTagEntities = await bookTagService.ListByBookAsync(bookId);
        }

    }
}
