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

        private string _Image = "";

        private List<string> Images = new List<string>();

        private Paginator<string> ImagePaginator = new Paginator<string>();


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
            await ImagePaginator.HandlePageIndexChange(args);

            _Image = ImagePaginator.Paged().ToList().FirstOrDefault();

            //Console.WriteLine(_Image);

            StateHasChanged();

            //await Refresh();
        }

        public async Task _sizing(PaginationEventArgs args)
        {
            await ImagePaginator.HandlePageSizeChange(args);

            _Image = ImagePaginator.Paged().FirstOrDefault();

            Console.WriteLine(_Image);

            StateHasChanged();

            //await Refresh();
        }

        private async Task reading()
        {
            foreach (var entry in entries)
            {

                Images.Add("data:image/*;base64," + ImageUtils.Read(entry));

                // StateHasChanged();
            }

            ImagePaginator.DataSource = Images;

            _Image = ImagePaginator.Paged().ToList().FirstOrDefault();

            StateHasChanged();

            await Task.CompletedTask;
        }

        private async Task preparing()
        {
            Console.WriteLine("识别文件中的有效图片.");

            string file = book.Path + "\\" + book.Name;

            FileInfo f = new FileInfo(file);

            if (!f.Exists)
            {
                Console.WriteLine("文件不存在...");
                return;
            }

            var archive = ArchiveFactory.Open(file);

            entries = archive.Entries
                    .Where(a => (! a.IsDirectory && ComicUtils.IsImage(a.Key)))
                    .OrderBy(a => a.Key)
                    .ToList();

            Console.WriteLine("找到[" + entries.Count + "]张图片");

            //Console.WriteLine
            await Task.CompletedTask;
        }


    }
}
