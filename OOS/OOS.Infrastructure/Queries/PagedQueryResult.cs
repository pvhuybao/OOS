using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace OOS.Infrastructure.Queries
{
    public class PagedQueryResult<T> : IPagedQueryResult
    {
        private const int DefaultPage = 1;

        private readonly IEnumerable<T> _items;
        private readonly long _totalItemCount;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly int _pageCount;

        public PagedQueryResult(IEnumerable<T> items, long totalItemCount, int page, int pageSize)
        {
            Contract.Requires(pageSize > 0);

            _items = items;
            _totalItemCount = totalItemCount;
            _pageSize = pageSize;
            _pageCount = (int)Math.Ceiling((double)TotalItemCount / PageSize);

            if (page < DefaultPage)
            {
                page = DefaultPage;
            }

            if (page > _pageCount)
            {
                page = _pageCount;
            }

            _page = page;
        }

        public IEnumerable<T> Items
        {
            get
            {
                return _items;
            }
        }

        public long TotalItemCount
        {
            get
            {
                return _totalItemCount;
            }
        }

        public int Page
        {
            get
            {
                return _page;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return Page > DefaultPage;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return Page < PageCount;
            }
        }

        public int StartItemIndex
        {
            get
            {
                int startItemIndex = ((Page - 1) * PageSize) + 1;
                if (startItemIndex > TotalItemCount)
                {
                    startItemIndex = (int)TotalItemCount;
                }

                return startItemIndex;
            }
        }

        public int EndItemIndex
        {
            get
            {
                int endItemIndex = Page * PageSize;
                if (endItemIndex > TotalItemCount)
                {
                    endItemIndex = (int)TotalItemCount;
                }

                return endItemIndex;
            }
        }
    }
}
