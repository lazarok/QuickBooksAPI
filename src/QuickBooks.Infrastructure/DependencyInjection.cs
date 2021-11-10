using Microsoft.Extensions.DependencyInjection;
using QuickBooks.Application;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Custom;
using QuickBooks.Infrastructure.QBFC;
using QuickBooks.Infrastructure.QBFC.Customer.GetAllCustomer;
using System;

namespace QuickBooks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IQbfcHandler, QbfcHandler>();

            services.AddTransient<IGetAllCustomerQbfcMessage, GetAllCustomerQbfcMessage>();

            return services;
        }
    }
}
