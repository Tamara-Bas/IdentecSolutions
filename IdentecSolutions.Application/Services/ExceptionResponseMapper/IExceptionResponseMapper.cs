using IdentecSolutions.Application.Contracts;

namespace IdentecSolutions.Application.Services.ExceptionResponseMapper
{
    public interface IExceptionResponseMapper
    {
        ExceptionResponse ExceptionResponse(Exception exception);
        ErrorResponse ErrorResponse(Exception exception);
    }
}
