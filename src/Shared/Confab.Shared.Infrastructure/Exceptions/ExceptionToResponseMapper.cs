using System;
using System.Collections.Concurrent;
using System.Net;
using Confab.Shared.Abstractions.Exceptions;
using Humanizer;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExToRespMapper2 : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception ex)
        {
            return new("ex to resp 2", HttpStatusCode.InternalServerError);
        }
    }

    internal class ExToRespMapper3 : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception ex)
        {
            return new("ex to resp 3", HttpStatusCode.InternalServerError);
        }
    }


    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static ConcurrentDictionary<Type, string> _codes = new();

        public ExceptionResponse Map(Exception ex) =>
            ex switch
            {
                ConfabException confabException => new ExceptionResponse(
                    Response: new ErrorResponse(Errors: new Error(Code: GetErrorCode(ex), Message: ex.Message)),
                    StatusCode: HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(
                    Response: new ErrorResponse(Errors: new Error(Code: "error", Message: "There was an error.")),
                    StatusCode: HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);

        private record ErrorResponse(params Error[] Errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            var code = type.Name.Underscore().Replace("_exception", string.Empty);
            return _codes.GetOrAdd(type, code);
        }
    }
}
