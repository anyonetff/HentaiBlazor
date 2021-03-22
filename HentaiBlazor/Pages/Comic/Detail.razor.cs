using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
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
    public partial class Detail
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        public CBookEntity book;


        private List<ZipArchiveEntry> entries;

        private List<string> images = new List<string>();


        protected override async Task OnInitializedAsync()
        {
            // _model.Pagination.Pagination.PageSize = 16;

            book = await bookService.FindAsync(Id);

            StateHasChanged();

            preparing();

            for (int i = 0; i < 5; i++)
            {
                images.Add("data:image/*;base64," + preview(entries[i]));

                StateHasChanged();
            }
        }

        private void preparing()
        {
            string file = book.Path + "\\" + book.Name;

            ZipArchive archive = ZipFile.OpenRead(file);

            entries = archive.Entries
                    .Where(a => ComicUtils.IsImage(a.FullName))
                    .OrderBy(a => a.FullName)
                    .ToList();
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
