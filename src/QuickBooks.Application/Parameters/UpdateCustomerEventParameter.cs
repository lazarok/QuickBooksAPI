using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Parameters
{
    public record UpdateCustomerEventParameter(
        string Name,
        string CompanyName,
        string Phone,
        string Email,
        string Contact,
        string Notes,
        bool? IsActive,
        string EditSequence);
}
