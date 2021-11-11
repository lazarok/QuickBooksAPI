using Microsoft.Extensions.DependencyInjection;
using QuickBooks.Application;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Infrastructure.QBFC;
using QuickBooks.Infrastructure.QBFC.Customer;
using System;

namespace QuickBooks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IQbfcHandler, QbfcHandler>();

            services.AddTransient<IGetAllCustomerQbfcMessage, GetAllCustomerQbfcMessage>();
            services.AddTransient<IGetCustomerByIdQbfcMessage, GetCustomerByIdQbfcMessage>();
            services.AddTransient<IAddCustomerQbfcMessage, AddCustomerQbfcMessage>();
            services.AddTransient<IUpdateCustomerQbfcMessage, UpdateCustomerQbfcMessage>();
            services.AddTransient<IGetCustomerByNameQbfcMessage, GetCustomerByNameQbfcMessage>();

            return services;
        }
    }
}
