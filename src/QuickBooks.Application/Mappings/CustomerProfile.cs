using AutoMapper;
using QuickBooks.Application.Handlers.Customer.GetAllCustomerEvent;
using QuickBooks.Application.Handlers.Customer.GetCustomerByIdEvent;
using QuickBooks.Application.Models.QBFC.Customer.GetAllCustomer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<GetAllCustomerResponse, GetAllCustomerEventDto>();
            CreateMap<GetCustomerByIdResponse, GetCustomerByIdEventDto>();

            CreateMap<GetAllCustomerEventCommand, GetAllCustomerRequest>();
            CreateMap<GetCustomerByIdEventCommand, GetCustomerByIdRequest>();
        }
    }
}
