using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal static class Extensions
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services) =>
            services.AddSingleton<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
            app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
