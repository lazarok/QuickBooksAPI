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
       string CompanyName,
       string Phone,
       string Email,
       string Contact,
       string Notes,
       bool IsActive,
       DateTime CreatedAt,
       string EditSequence);
}
