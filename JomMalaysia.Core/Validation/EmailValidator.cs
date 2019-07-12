using System;
using System.Linq;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.FluentValidation;


namespace JomMalaysia.Core.Validation
{
    public class EmailValidator: AbstractValidator<Email>
    {
       public EmailValidator()
        {
            RuleFor(x => x.User)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NoStartWithWhiteSpace()
                .Must(NotUpper).WithMessage("{PropertyName} must be in lowerCase")
                .Matches(@"[^\s]");    //WhiteSpace            
            RuleFor(x => x.Domain)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NoStartWithWhiteSpace()
                .Must(NotUpper).WithMessage("{PropertyName} must be in lowerCase")
                .Must(BeAValidDomain).WithMessage("{PropertyName} is invalid")
                .Matches(@"[^\s]");

        }

        protected bool BeAValidDomain(string domain)
        {
            return domain.Contains("@");
        }
        
        protected bool NotUpper(string user)
        {
            if (user.All(Char.IsUpper)) { return false; }
            return true;
        }
    }
}
