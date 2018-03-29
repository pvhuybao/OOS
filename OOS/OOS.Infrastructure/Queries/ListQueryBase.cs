using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Infrastructure.Queries
{
    public abstract class ListQueryBase : QueryBase
    {
        /// <summary>
        /// Gets or sets the page size, default is 15 per page.
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// Gets or sets the page index of current page, default is 1.
        /// </summary>
        public int Page { get; set; } = 1;
    }
}