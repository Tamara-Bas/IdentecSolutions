using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentecSolutions.Domain.Exceptions
{
    public sealed class ValidationException :ApplicationException
    {
        public ValidationException():base("Validation Failure", "One or more validation errors occured")
        {
            ErrorsDictionary = new Dictionary<string, string[]>();
        }
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            :base("Validation Failure", "One or more validation errors occured")
        {
            ErrorsDictionary = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            ErrorsDictionary = failures.GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                }
                ).ToDictionary(x => x.Key, x => x.Values);
        }

        public ValidationException(string message) : base(message)
        {
            ErrorsDictionary = new Dictionary<string, string[]>()
            {
                {"error", new string[] {message} },
            };
        }

        public ValidationException(string message, Exception innerException) : base(message,innerException)
        {
            ErrorsDictionary = new Dictionary<string, string[]>()
            {
                {"error", new string[] {message} },
            };
        }
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
