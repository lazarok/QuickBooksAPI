using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using QuickBooks.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace QuickBooks.API.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app, IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void UseLocalizationExtension(this IApplicationBuilder app)
        {
            var cultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("es")
            };

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
        }
    }
}
