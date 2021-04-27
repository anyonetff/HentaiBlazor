using AntDesign;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Ezcomp;
using HentaiBlazor.Services;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using SharpCompress.Archives;
using SharpCompress.Common;
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
        public CoverService coverService { get; set; }

        [Inject]
        public ReaderService readerService { get; set; }

        [Inject]
        public CryptoService cryptoService { get; set; }

        [Inject] 
        public MessageService _message { get; set; }


        public CBookEntity book;


        private List<IArchiveEntry> entries = new List<IArchiveEntry>();

        private List<IArchiveEntry> entry = new List<IArchiveEntry>();

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
                await reader();
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

            await Paging(new PaginationEventArgs(EntryPaginator.PageIndex - 1, 1));
            
        }

        public async Task Next()
        {
            Console.WriteLine("下一页");

            if (EntryPaginator.PageIndex == EntryPaginator.Total)
            {
                await _message.Warning("已经是最末页了");

                return;
            }

            await Paging(new PaginationEventArgs(EntryPaginator.PageIndex + 1, 1));

        }

        public async Task OnCovered()
        {
            Console.WriteLine("设置该页为封面");

            foreach (var e in entry)
            {
                book.Cover = e.Key;
                await bookService.SaveAsync(book);

                coverService.RemoveCached(book.Id);

                break;
            }

            await _message.Warning("已将该页设置为了封面.");
        }

        public async Task OnPreview()
        {
            _Paged = false;

            var args = new PaginationEventArgs(1, 20);

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

                    var args = new PaginationEventArgs(i + 1, 1);

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

        public async Task Paging(PaginationEventArgs args)
        {
            Console.WriteLine(args.Page);

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
                    return ImageUtils.Read(e);
                }

                return ImageUtils.PreviewInBase64(e, 320, 360);
            });
        }

        private async Task reader()
        {
            List<BCryptoEntity> cs = await this.cryptoService.SearchAsync("");

            await Task.Run(() =>
            {
                entries = this.readerService.Images(book, cs);
            });

            Console.WriteLine("文件页数:" + entries.Count);

            // 过滤不是图片的压缩包文件，并按文件名排序

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
                await EntryPaginator.HandlePageIndexChange(new PaginationEventArgs(book.Index, 1));
            }

            entry = EntryPaginator.Paged().ToList();
        }



    }
}
