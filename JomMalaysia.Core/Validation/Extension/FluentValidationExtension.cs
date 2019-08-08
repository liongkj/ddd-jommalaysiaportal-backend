using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace JomMalaysia.Core.Validation.Extension
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, string> NoStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => x != null && !x.StartsWith(" ")).WithMessage("{PropertyName} should not start with white space";
        }

       
    }
}
