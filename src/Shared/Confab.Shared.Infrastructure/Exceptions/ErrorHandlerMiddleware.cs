using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception ex)
        {
            var errorResponse = new {code = ex.GetType().Name, message = ex.Message};
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
