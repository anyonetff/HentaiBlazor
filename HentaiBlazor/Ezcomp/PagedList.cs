using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public class PagedList<E>
    {
        public List<E> Data { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int PageCount { get; set; }

        public int TotalCount { get; set; }

        public ValueTuple Total { get; set; }

        public PagedList() 
        {
            PageSize = 10;
            PageIndex = 1;
        }

        public PagedList(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public PagedList(int pageSize, int pageIndex, int totalCount)
        {
            PageSize = pageSize <= 0 ? 10 : pageSize;
            TotalCount = totalCount < 0 ? 0 : totalCount;

            PageCount = TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1);

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > PageCount)
            {
                pageIndex = PageCount;
            }

            PageIndex = pageIndex;
        }

        public int Skip()
        {
            return PageSize * (PageIndex - 1);
        }

        public int Take()
        {
            return PageSize;
        }

    }
}
