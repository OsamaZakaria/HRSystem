using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using HRSystem.Application.Core.Exceptions;
using HRSystem.Domain.Core;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Exceptions;
using HRSystem.Domain.Core.Primitives;
using HRSystem.Web.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRSystem.Web.Middleware
{
    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = (int)httpStatusCode;

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), serializerOptions);

            await httpContext.Response.WriteAsync(response);
        }

        private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception exception) =>
            exception switch
            {
                ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors),
                DomainException domainException => (HttpStatusCode.BadRequest, new[] { domainException.Error }),
                _ => (HttpStatusCode.InternalServerError, new[] { DomainErrors.General.ServerError })
            };
    }

    internal static class ExceptionHandlerMiddlewareExtensions
    {
        internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
