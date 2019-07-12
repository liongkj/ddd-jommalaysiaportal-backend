using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, string> NoStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => x != null && !x.StartsWith(" ")).WithMessage("{PropertyName} should not start with white space";
        }
    }
}
