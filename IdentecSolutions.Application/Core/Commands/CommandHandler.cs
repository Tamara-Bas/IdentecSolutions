using MediatR;

namespace IdentecSolutions.Application.Core.Commands
{
    public abstract class CommandHandler<TRequest> : ICommandHandler<TRequest> where TRequest : class, ICommand
    {
        public abstract Task Handle(TRequest request, CancellationToken cancellationToken);

    }
}
