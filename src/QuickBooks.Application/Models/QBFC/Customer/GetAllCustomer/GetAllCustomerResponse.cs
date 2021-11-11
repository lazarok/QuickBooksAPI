using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Models.QBFC.Customer.GetAllCustomer
{
    public record GetAllCustomerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string EditSequence { get; set; }
    }
}
