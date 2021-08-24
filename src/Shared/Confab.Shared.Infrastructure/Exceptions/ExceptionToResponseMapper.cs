using System;
using System.Collections.Concurrent;
using System.Net;
using Confab.Shared.Abstractions.Exceptions;
using Humanizer;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> _codes = new();

        public ExceptionResponse Map(Exception ex)
        {
            return ex switch
            {
                ConfabException confabEx => new ExceptionResponse(
                    new ErrorResponse(Errors: new Error(GetErrorCode(confabEx), ex.Message)),
                    HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(
                    new ErrorResponse(Errors: new Error("error", "There was an error.")),
                    HttpStatusCode.InternalServerError)
            };
        }

        private static string GetErrorCode(Exception ex)
        {
            var type = ex.GetType();
            if (_codes.TryGetValue(type, out var code))
                return code;

            code = type.Name.Underscore().Replace("_exception", string.Empty);
            return _codes.GetOrAdd(type, code);
        }

        private record Error(string Code, string Message);

        private record ErrorResponse(params Error[] Errors);
    }
}