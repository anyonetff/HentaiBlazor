using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public class Paginator<TItem>
    {

        public Paginator()
        {

        }

        public Paginator(int pageSize)
        {
            this._pageSize = pageSize;
        }

        //[Parameter]
        public IEnumerable<TItem> DataSource
        {
            get => _dataSource;
            set
            {
                _waitingReload = true;
                _dataSourceCount = value?.Count() ?? 0;
                _dataSource = value ?? Enumerable.Empty<TItem>();
            }
        }

        // public IEnumerable<TItem> PagedDataSource;


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

        private bool _waitingReload;
        private IEnumerable<TItem> _dataSource;
        private int _dataSourceCount;

        private int _total;
        private string _paginationPosition = "bottomRight";
        private string _paginationClass;
        private int _pageIndex = 1;
        private int _pageSize = 10;

        public int offset()
        {
            return _pageSize * (_pageIndex - 1);
        }

        public int limit()
        {
            return _pageSize;
        }

        public IEnumerable<TItem> Paged()
        {
            Console.WriteLine("返回分页的数据");

            return _dataSource
                .Skip(offset())
                .Take(limit())
                .ToList();
        }

        public async Task HandlePageIndexChange(PaginationEventArgs args)
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

            //Paged();

            //ReloadAndInvokeChange();

            //StateHasChanged();
        }

        public async Task HandlePageSizeChange(PaginationEventArgs args)
        {
            _pageSize = args.PageSize;

            //ReloadAndInvokeChange();
            //Paged();

            if (PageSizeChanged.HasDelegate)
            {
                await PageSizeChanged.InvokeAsync(args.PageSize);
            }

            if (OnPageSizeChange.HasDelegate)
            {
                await OnPageSizeChange .InvokeAsync(args);
            }
        }

    }
}
