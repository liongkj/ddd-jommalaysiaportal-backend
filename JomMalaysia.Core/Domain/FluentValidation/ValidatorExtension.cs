using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace JomMalaysia.Core.Domain.FluentValidation
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, string> validNumber<T, TProperty>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ValidNumberValidator());
        }
    }

    public class ValidNumberValidator: RegularExpressionValidator
    {
        public ValidNumberValidator() : base("^[0-9]{6}[0-Z][A-Z]$") { }
    }
}
