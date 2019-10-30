using System;
using System.Linq;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.Validation
{
    public class ContactValidator : AbstractValidator<ContactRequest>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid."); //check from EmailValidator
            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} should not be blank")
                .Must(BeAValidNumber)

                ; //check from PhoneValidator
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} should not be blank")
                .Length(2, 25);
            //check from NameValidator

        }


        protected bool BeAValidNumber(string number)
        {
            number = number.Replace("-", "");
            var IsDigit = number.All(Char.IsDigit);
            //var StartsWith = number.StartsWith("0");
            var ValidLength = number.Length >= 10 && number.Length <= 11;
            return IsDigit;



        }


    }
}
