using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.FluentValidation;
using System;
using FluentValidation.Validators;
using System.Linq;

namespace JomMalaysia.Core.Validation
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Number)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NoStartWithWhiteSpace()
                .Must(BeAnInteger).WithMessage("{PropertyName} can only contain digits")
                .Must(BeAValidNumber).WithMessage("{PropertyName} is invalid")
                .MinimumLength(11);
            

        }

        protected bool BeAValidNumber(string number)
        {
            return number.StartsWith("0");

        }

        protected bool BeAnInteger(string number)
        {
            number = number.Replace("-", "");
            return number.All(Char.IsDigit);
        }
        
    }
    
}

  
