using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.GetAllCustomerEvent
{
    public record GetAllCustomerEventDto(
        string Id,
        string Name,
        string CompanyName,
        string EditSequence);
}
