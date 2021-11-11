using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Parameters
{
    public class RequestParameter
    {
        static readonly int pageNumberDefault = 1;
        static readonly int pageSizeDefault = 10;

        /// <summary>
        /// Indicates the current page number.
        /// </summary>
        /// 
        [DefaultValue(1)]
        public int PageNumber { get; set; } = pageNumberDefault;

        /// <summary>
        /// Indicates the number of items per page.
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = pageSizeDefault;

        /// <summary>
        /// 
        /// </summary>
        public RequestParameter()
        {
            PageNumber = pageNumberDefault;
            PageSize = pageSizeDefault;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber">Indicates the current page number.</param>
        /// <param name="pageSize">Indicates the number of items per page.</param>
        public RequestParameter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < pageNumberDefault ? pageNumberDefault : pageNumber;
            PageSize = pageSize < pageSizeDefault ? pageSizeDefault : pageSize;
        }
    }
}
