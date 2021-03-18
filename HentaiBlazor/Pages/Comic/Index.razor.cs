using AntDesign;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Index
    {

        [Inject]
        public BookService bookService { get; set; }

        private List<CBookEntity> CBookEntities;

        //private List<CBookEntity> _CBookEntities;

        private Paginator<CBookEntity> BookPaginator = new Paginator<CBookEntity>();

        protected override async Task OnInitializedAsync()
        {
            // _model.Pagination.Pagination.PageSize = 16;

            CBookEntities = await bookService.ListAsync();

            BookPaginator.DataSource = CBookEntities;
        }

        private async Task RefreshCover()
        {
            Console.WriteLine("刷新当页封面");

            List<CBookEntity> _CBookEntities = BookPaginator.Paged().ToList();

            for (int i = 0; i < _CBookEntities.Count; i++)
            {
                CBookEntity _book = _CBookEntities.ElementAt(i);
                _book.Cover = "/book/cover.jpg";
                
                //_book = await ReadCover(_book);
                StateHasChanged();
            }
        }


        private async Task<CBookEntity> ReadCover(CBookEntity book)
        {
            return await Task.Run(() =>
            {
                if (book.Cover != "/book/cover.jpg")
                {
                    return book;
                }

                string file = book.Path + "\\" + book.Name;

                ZipArchive archive = ZipFile.OpenRead(file);

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Console.WriteLine(" - " + entry.FullName);

                    if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        string cover = "data:image/*;base64," + preview(entry);
                        book.Cover = cover;

                        // Console.WriteLine(cover);

                        break;
                    }
                }

                return book;
            });
        }

        protected string preview(ZipArchiveEntry entry)
        {
            var stream = entry.Open();
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                bytes = ms.ToArray();

                return Convert.ToBase64String(bytes);
            }
        }
    }
}
