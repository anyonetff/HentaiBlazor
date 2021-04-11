using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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


        [Inject] 
        public MessageService _message { get; set; }


        public CBookEntity book;


        private List<IArchiveEntry> entries;

        private List<IArchiveEntry> entry;

        private string ModeFit = "fit";

        private string ModeWidth = "width";

        private string ModeHeight = "heigth";

        private string _Mode = "fit";

        private bool _Paged = true;

        private bool _Scale = true;

        private string _Image = "";

        // private List<string> _Images = new List<string>();

        private Dictionary<string, string> _Cache = new Dictionary<string, string>();

        // private List<string> Images = new List<string>();

        private Paginator<IArchiveEntry> EntryPaginator = new Paginator<IArchiveEntry>(1);


        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("读取漫画详情");

            book = await bookService.FindAsync(Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await preparing();
                await reading();
            }
        }

        public async Task Previous()
        {
            Console.WriteLine("上一页");

            if (EntryPaginator.PageIndex == 1) 
            {
                await _message.Warning("已经是第一页了");

                return;
            }

            await _paging(new PaginationEventArgs { PageIndex = EntryPaginator.PageIndex - 1 });
            
        }

        public async Task Next()
        {
            Console.WriteLine("下一页");

            if (EntryPaginator.PageIndex == EntryPaginator.Total)
            {
                await _message.Warning("已经是最末页了");

                return;
            }

            await _paging(new PaginationEventArgs { PageIndex = EntryPaginator.PageIndex + 1 });

        }

        public async Task OnPreview()
        {
            _Paged = false;

            var args = new PaginationEventArgs { PageSize = 20, PageIndex = 1 };

            await EntryPaginator.HandlePageSizeChange(args);
            await EntryPaginator.HandlePageIndexChange(args);

            entry = EntryPaginator.Paged().ToList();

            StateHasChanged();

            await reading();
        }

        public async Task OnRead(string key)
        {
            _Paged = true;

            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                if (StringUtils.IsEqual(key, e.Key))
                {
                    Console.WriteLine("快速定位页面[" + i + "]");

                    var args = new PaginationEventArgs { PageSize = 1, PageIndex = i + 1 };

                    await EntryPaginator.HandlePageSizeChange(args);
                    await EntryPaginator.HandlePageIndexChange(args);

                    break;
                }
            }

            entry = EntryPaginator.Paged().ToList();

            StateHasChanged();

            await reading();
        }

        public void OnMode(string mode) 
        {
            _Mode = mode;
        }

        public void OnScale()
        {
            _Scale = ! _Scale;
            
        }

        public async Task _paging(PaginationEventArgs args)
        {
            await EntryPaginator.HandlePageIndexChange(args);

            entry = EntryPaginator.Paged().ToList();

            Console.WriteLine(entry.First().Key);

            if (_Paged)
            {
                book.Index = EntryPaginator.PageIndex;
                await bookService.UpdateAsync(book);
            }

            StateHasChanged();

            await reading();
        }

        public async Task _sizing(PaginationEventArgs args)
        {
            await EntryPaginator.HandlePageSizeChange(args);

            entry = EntryPaginator.Paged().ToList();

            StateHasChanged();

            await reading();
        }


        private async Task reading()
        {
            _Image = "";
            // _Images.Clear();

            foreach (var e in entry)
            {
                if (_Paged)
                {
                    _Image = await dataURL(e);
                }
                else 
                {
                    if (! _Cache.ContainsKey(e.Key))
                    {
                        _Cache.Add(e.Key, await dataURL(e));
                    }
                }

                StateHasChanged();
            }
        }

        private async Task<string> dataURL(IArchiveEntry e) {
            return await Task<string>.Run(() =>
            {
                if (_Paged)
                {
                    return "data:image/*;base64," + ImageUtils.Read(e);
                }

                return "data:image/*;base64," + ImageUtils.PreviewInBase64(e, 320, 360);
            });
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

            if (book.Count != entries.Count)
            {
                Console.WriteLine("更新页码数.");

                book.Count = entries.Count;

                await bookService.UpdateAsync(book);
            }
            
            
            Console.WriteLine("找到[" + entries.Count + "]张图片");

            EntryPaginator.DataSource = entries;

            if (_Paged && book.Index > 0 && book.Index <= entries.Count)
            {
                await EntryPaginator.HandlePageIndexChange(new PaginationEventArgs { PageIndex = book.Index });
            }

            entry = EntryPaginator.Paged().ToList();

            //Console.WriteLine
            await Task.CompletedTask;
        }


    }
}
