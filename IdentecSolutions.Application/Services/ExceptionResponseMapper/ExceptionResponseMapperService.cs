using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Domain.Exceptions;

using System.Net;
using ValidationException = IdentecSolutions.Domain.Exceptions.ValidationException;

namespace IdentecSolutions.Application.Services.ExceptionResponseMapper
{
    public class ExceptionResponseMapperService : IExceptionResponseMapper
    {
        public ErrorResponse ErrorResponse(Exception exception)
        {

            return exception switch
            {
                ValidationException ex => new ErrorResponse("validation_error", ex.ErrorsDictionary),
                InvalidDataException ida => new ErrorResponse("validation_error", ida.Message),
                NotFoundException nfe => new ErrorResponse("resource_not_founr_error", nfe.Message),
                _ => new ErrorResponse("error", exception.Message)
            };
          
        }

        public ExceptionResponse ExceptionResponse(Exception exception)
        {
            var errorResponse = ErrorResponse(exception);
            var statusCode = exception switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                InvalidDataException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
            return new ExceptionResponse(errorResponse, statusCode);
        }
    }
}
