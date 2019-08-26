using System;
using System.Linq;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Validation.Extension;

namespace JomMalaysia.Core.Validation
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NoStartWithWhiteSpace()
                .MaximumLength(50)
                .Must(BeAValidName).WithMessage("{PropertyName} is invalid.");
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NoStartWithWhiteSpace()
                .Length(2, 30)
                .Must(BeAValidName).WithMessage("{PropertyName} is invalid.");

        }

        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            name = name.Replace(".", "");
            return name.All(Char.IsLetter);
        }
    }
}
