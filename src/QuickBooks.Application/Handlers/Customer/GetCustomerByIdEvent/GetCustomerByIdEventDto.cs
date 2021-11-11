using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.GetCustomerByIdEvent
{
    public record GetCustomerByIdEventDto(
        string Id,
        string Name,
        string Phone);
}
