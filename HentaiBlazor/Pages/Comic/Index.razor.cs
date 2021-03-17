using AntDesign;
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
    public partial class Index
    {

        [Inject]
        public BookService bookService { get; set; }

        private List<CBookEntity> CBookEntities;

        [Parameter]
        public int Total
        {
            get => _total > _dataSourceCount ? _total : _dataSourceCount;
            set
            {
                _total = value;
            }
        }

        [Parameter]
        public EventCallback<int> TotalChanged { get; set; }

        [Parameter]
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (_pageIndex != value)
                {
                    _pageIndex = value;
                    _waitingReloadAndInvokeChange = true;
                }
            }
        }

        [Parameter]
        public EventCallback<int> PageIndexChanged { get; set; }

        [Parameter]
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    _waitingReloadAndInvokeChange = true;
                }
            }
        }

        bool _waitingReloadAndInvokeChange;

        [Parameter]
        public EventCallback<int> PageSizeChanged { get; set; }

        [Parameter]
        public EventCallback<PaginationEventArgs> OnPageIndexChange { get; set; }

        [Parameter]
        public EventCallback<PaginationEventArgs> OnPageSizeChange { get; set; }

        private int _total;
        private int _dataSourceCount;
        private string _paginationPosition = "bottomRight";
        private string _paginationClass;
        private int _pageIndex = 1;
        private int _pageSize = 16;

        private List<CBookEntity> _CBookEntities;

        protected override async Task OnInitializedAsync()
        {
            // _model.Pagination.Pagination.PageSize = 16;

            CBookEntities = await bookService.ListAsync();

            _dataSourceCount = CBookEntities.Count();

            this.ReloadAndInvokeChange();
        }

        private async Task HandlePageIndexChange(PaginationEventArgs args)
        {
            _pageIndex = args.PageIndex;

            if (PageIndexChanged.HasDelegate)
            {
                await PageIndexChanged.InvokeAsync(args.PageIndex);
            }

            if (OnPageIndexChange.HasDelegate)
            {
                await OnPageIndexChange.InvokeAsync(args);
            }

            ReloadAndInvokeChange();

            StateHasChanged();
        }

        private void HandlePageSizeChange(PaginationEventArgs args)
        {
            _pageSize = args.PageSize;

            ReloadAndInvokeChange();

            if (PageSizeChanged.HasDelegate)
            {
                PageSizeChanged.InvokeAsync(args.PageSize);
            }

            if (OnPageSizeChange.HasDelegate)
            {
                OnPageSizeChange.InvokeAsync(args);
            }
        }

        private void ReloadAndInvokeChange()
        {
            _CBookEntities = this.CBookEntities.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            //StateHasChanged();
            /*
            _ = InvokeAsync(() => 
            {
                for (int i = 0; i < _CBookEntities.Count; i++)
                {
                    ReadCover(_CBookEntities.ElementAt(i));
                    StateHasChanged();
                }
            });
            */
        }

        private void ReadCover(CBookEntity book)
        {

            if (book.Cover != "/book/cover.jpg") 
            {
                return ;
            }

            string file = book.Path + "\\" + book.Name;



            ZipArchive archive = ZipFile.OpenRead(file);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                Console.WriteLine(" - " + entry.FullName);

                if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    string cover = "data:image/*;base64," + preview(entry);
                    book.Cover = cover;

                    Console.WriteLine(cover);

                    break;
                }
            }


            return ;
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
