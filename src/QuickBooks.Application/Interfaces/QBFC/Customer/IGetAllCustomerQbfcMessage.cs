using QuickBooks.Application.Models.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetAllCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC.Customer
{
    public interface IGetAllCustomerQbfcMessage : IQbfcMessage<GetAllCustomerRequest, IEnumerable<GetAllCustomerResponse>>
    {
    }
}
