using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions;

internal class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionCompositionRoot _exceptionCompositionRoot;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _exceptionCompositionRoot = exceptionCompositionRoot;
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
        var errorResponse = _exceptionCompositionRoot.Map(ex);
        context.Response.StatusCode = (int) (errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;
        if (response is null)
            return;

        await context.Response.WriteAsJsonAsync(response);
    }
}