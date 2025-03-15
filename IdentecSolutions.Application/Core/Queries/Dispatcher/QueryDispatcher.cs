using MediatR;

namespace IdentecSolutions.Application.Core.Queries.Dispatcher
{
    public sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IMediator _mediator;

        public QueryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        }
    }
}
