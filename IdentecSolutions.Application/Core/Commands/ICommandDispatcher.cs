namespace IdentecSolutions.Application.Core.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command, CancellationToken cancellation) where T : ICommand;
        Task<TResponse> SendAsync<TResponse, T>(T command, CancellationToken cancellation) where T : ICommand<TResponse>;
    }
}
