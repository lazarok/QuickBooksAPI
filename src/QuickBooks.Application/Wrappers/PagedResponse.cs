using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Wrappers
{
    public class PagedResponse<T>
    {
        static readonly int pageNumberDefault = 1;
        static readonly int pageSizeMin = 10;

        /// <summary>
        /// Indicates the current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Indicates the total pages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Indicates the total rows.
        /// </summary>
        public int Total { get; }

        public List<T> Data { get; set; }

        public PagedResponse(IEnumerable<T> collection, int pageNumber = 1, int pageSize = 10)
            : base()
        {
            PageSize = pageSize < pageSizeMin ? pageSizeMin : pageSize;
            PageNumber = pageNumber < pageNumberDefault ? pageNumberDefault : pageNumber;

            Data =
                collection
                    .Skip(PageSize * (PageNumber - 1))
                    .Take(PageSize)
                    .ToList();

            TotalPages = Convert.ToInt32(Math.Ceiling(collection.Count() / Convert.ToDecimal(PageSize)));
            Total = collection.Count();
        }
    }
}
