using QuickBooks.Application.Models.QBFC.Customer.GetCustomerByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC.Customer
{
    public interface IGetCustomerByNameQbfcMessage : IQbfcMessage<GetCustomerByNameRequest, GetCustomerByNameResponse>
    {
    }
}
