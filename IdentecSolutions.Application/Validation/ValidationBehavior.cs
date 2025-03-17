using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = IdentecSolutions.Domain.Exceptions.ValidationException;

namespace IdentecSolutions.Application.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next().ConfigureAwait(false);
            }
            var context = new ValidationContext<TRequest>(request);


            //var failures = _validators
            //    .Select(v => v.Validate(context))
            //    .SelectMany(result => result.Errors)
            //    .Where(error => error != null)
            //    .ToList();

            //if (failures.Any())
            //{
            //    throw new ValidationException(failures);
            //}
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

            var failures = validationResults
                .Where(f => f.Errors.Any())
                .SelectMany(f => f.Errors)
                .ToList();

            if (failures.Any())
            {
                _logger.LogInformation("Invalid validation for request: {request}", request);
                throw new ValidationException(failures);
            }
            return await next().ConfigureAwait(false);
        }
    }
}
