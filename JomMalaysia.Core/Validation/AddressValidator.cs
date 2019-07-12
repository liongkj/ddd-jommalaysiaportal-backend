using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class AddressValidator: AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Add1).NotEmpty();
            RuleFor(x => x.Add2).NotEmpty();
            RuleFor(x => x.State)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(x => x.City)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(x => x.Country)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30)
                .NotEmpty();
            RuleFor(x => x.PostalCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(10)
                .NotEmpty();

            //update
            RuleFor(c => c.PostalCode).Matches(@"^\d{5}$")
                .When(c => c.Country == "Malaysia")
                .WithMessage("Malaysian Postcodes have 5 digits");
            
        }
    }
}
