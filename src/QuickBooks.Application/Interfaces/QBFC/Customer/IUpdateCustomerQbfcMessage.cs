using QuickBooks.Application.Models.QBFC.Customer.UpdateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC.Customer
{
    public interface IUpdateCustomerQbfcMessage : IQbfcMessage<UpdateCustomerRequest, UpdateCustomerResponse>
    {
    }
}
