using MediatR;
namespace IdentecSolutions.Application.Core.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<out TResponse> : IQuery, IRequest<TResponse> { }
}
