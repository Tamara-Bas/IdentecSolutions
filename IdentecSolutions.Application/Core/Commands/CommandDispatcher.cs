using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentecSolutions.Application.Core.Commands
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;
        public CommandDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : ICommand
        {
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> SendAsync<TResponse, T>(T command, CancellationToken cancellationToken) where T : ICommand<TResponse>
        {
            return await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        }
    }
}
