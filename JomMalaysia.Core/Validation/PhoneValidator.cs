using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.FluentValidation;
using System;
using FluentValidation.Validators;

namespace JomMalaysia.Core.Validation
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Number).NotEmpty().MinimumLength(9);

        }
    }
    
}

  
