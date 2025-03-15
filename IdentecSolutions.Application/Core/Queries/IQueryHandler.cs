using MediatR;

namespace IdentecSolutions.Application.Core.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> :IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
