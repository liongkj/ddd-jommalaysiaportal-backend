using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using JomMalaysia.Core.Validation;
using JomMalaysia.Core.Validation.Extension;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().Length(5, 15).WithMessage("{ PropertyName} should be not be blank and have between 5 to 15 characters");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Name).Must(BeAValidName).NotEmpty()
                .NoStartWithWhiteSpace()
                .Length(2, 50)
                .WithMessage("{PropertyName} should have length of between 2 characters to maximum 50 characters.");
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