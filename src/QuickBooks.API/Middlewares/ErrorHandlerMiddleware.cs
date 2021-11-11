using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using QuickBooks.Application.Wrappers;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using QuickBooks.Application.Exceptions;
using Microsoft.Extensions.Localization;
using QuickBooks.Resources.Infrastructure;

namespace QuickBooks.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<I18n> _localizer;

        public ErrorHandlerMiddleware(RequestDelegate next, IStringLocalizer<I18n> localizer)
        {
            _next = next;
            _localizer = localizer;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ErrorResponse(error?.Message);

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)e.StatusCode;
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        responseModel.Message = _localizer["UnexpectedError"];
                        responseModel.Errors = new List<string>() { error?.Message };
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
