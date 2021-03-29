using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Service.Comic;
using Microsoft.AspNetCore.Components;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Comic
{
    public partial class Viewer
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        public CBookEntity book;


        private List<IArchiveEntry> entries;

        private List<IArchiveEntry> entry;


        private string _Image = "";

        // private List<string> Images = new List<string>();

        private Paginator<IArchiveEntry> EntryPaginator = new Paginator<IArchiveEntry>(1);


        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("读取漫画详情");

            book = await bookService.FindAsync(Id);


            //_Image = ImagePaginator.Paged().ToList().FirstOrDefault();

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await preparing();
                await reading();
            }
        }

        public async Task _paging(PaginationEventArgs args)
        {
            await EntryPaginator.HandlePageIndexChange(args);

            entry = EntryPaginator.Paged().ToList();

            Console.WriteLine(entry.First().Key);

            await reading();

            StateHasChanged();
        }

        public async Task _sizing(PaginationEventArgs args)
        {
            await EntryPaginator.HandlePageSizeChange(args);

            entry = EntryPaginator.Paged().ToList();

            StateHasChanged();
        }

        private async Task reading()
        {
            foreach (var e in entry)
            {

                _Image = "data:image/*;base64," + ImageUtils.Read(e);

                // StateHasChanged();
            }

            StateHasChanged();

            await Task.CompletedTask;
        }

        private async Task preparing()
        {
            Console.WriteLine("识别文件中的有效图片.");

            string file = Path.Combine(book.Path, book.Name);

            FileInfo f = new FileInfo(file);

            if (!f.Exists)
            {
                Console.WriteLine("文件不存在...");
                return;
            }

            var archive = ArchiveFactory.Open(file);

            // 过滤不是图片的压缩包文件，并按文件名排序
            entries = archive.Entries
                    .Where(a => (! a.IsDirectory && ComicUtils.IsImage(a.Key)))
                    .OrderBy(a => a.Key)
                    .ToList();

            Console.WriteLine("找到[" + entries.Count + "]张图片");

            EntryPaginator.DataSource = entries;
            entry = EntryPaginator.Paged().ToList();

            //Console.WriteLine
            await Task.CompletedTask;
        }


    }
}
