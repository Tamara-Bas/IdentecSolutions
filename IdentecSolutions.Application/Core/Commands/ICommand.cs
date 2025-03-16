using MediatR;
namespace IdentecSolutions.Application.Core.Commands
{
    public interface ICommand :IRequest
    {
        public Guid? CommandId => Guid.NewGuid();
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        public Guid? CommandId => Guid.NewGuid();
    }
}
