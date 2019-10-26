using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace JomMalaysia.Core.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
            : base("User is not authorized to use this application.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public NotAuthorizedException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
