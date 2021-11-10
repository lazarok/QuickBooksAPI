using QuickBooks.Application.Models.QBFC.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC.Custom
{
    public interface IGetAllCustomerQbfcMessage : IQbfcMessage<IEnumerable<GetAllCustomerResponse>>
    {
    }
}
