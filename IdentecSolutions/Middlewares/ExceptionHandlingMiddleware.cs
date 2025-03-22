using IdentecSolutions.Application.Services.ExceptionResponseMapper;
using System.Text.Json;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;
    //{
    //    _next = next;
    //}

    public async Task InvokeAsync(HttpContext context, 
        ILogger<ExceptionHandlingMiddleware> logger,
        IExceptionResponseMapper exceptionResponseMapper)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
           // await HandleExceptionAsync(context, ex);
            logger!.LogError(ex, ex.Message);
            var exceptionResponse = exceptionResponseMapper!.ExceptionResponse(ex);
            context!.Response.ContentType = "applications/json";
            context!.Response.StatusCode = (int)exceptionResponse.StatusCode;
            await context!.Response.WriteAsync(JsonSerializer.Serialize(exceptionResponse.Response)).ConfigureAwait(false);
        }
    }

    //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    //{
    //    var response = context.Response;
    //    response.ContentType = "application/json";

    //    var errorResponse = new { message = "An unexpected error occurred." };
    //    var statusCode = HttpStatusCode.InternalServerError;

    //    switch (exception)
    //    {
    //        case ValidationException validationException:
    //            statusCode = HttpStatusCode.BadRequest;
    //            errorResponse = new
    //            {
    //                message = "Validation failed",
    //                errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
    //            };
    //            break;

    //        case KeyNotFoundException:
    //            statusCode = HttpStatusCode.NotFound;
    //            errorResponse = new { message = exception.Message };
    //            break;

    //        default:
    //            // Log exception (optional)
    //            Console.WriteLine(exception);
    //            break;
    //    }

    //    response.StatusCode = (int)statusCode;
    //    var result = JsonSerializer.Serialize(errorResponse);
    //    return response.WriteAsync(result);
    //}
}