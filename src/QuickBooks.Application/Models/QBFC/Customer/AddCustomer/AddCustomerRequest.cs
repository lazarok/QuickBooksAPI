using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Models.QBFC.Customer.AddCustomer
{
    public record AddCustomerRequest(
        string Name,      
        string CompanyName,
        string Phone,
        string Email,
        string Contact,
        string Notes,
        bool? IsActive);
}
