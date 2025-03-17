
using System.Net;

namespace IdentecSolutions.Application.Contracts
{
   public record ExceptionResponse(ErrorResponse Response, HttpStatusCode StatusCode);

}
