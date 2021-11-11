using QuickBooks.Application.Models.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC.Customer
{
    public interface IGetCustomerByIdQbfcMessage : IQbfcMessage<GetCustomerByIdRequest,GetCustomerByIdResponse>
    {
    }
}
