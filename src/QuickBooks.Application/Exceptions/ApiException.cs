using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message)
        => StatusCode = code;

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}
