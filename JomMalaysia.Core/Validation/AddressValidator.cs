using FluentValidation;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.Validation
{
    public class AddressValidator : AbstractValidator<AddressRequest>
    {
        public AddressValidator()
        {
            //RuleFor(x => x.Location).SetValidator(new LocationValidator());

            RuleFor(x => x.Add1).NotEmpty();
            RuleFor(x => x.Add2); //optional
            RuleFor(x => x.City)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();


            RuleFor(x => x.State).IsInEnum();

            RuleFor(x => x.Country)
                .IsInEnum();
            RuleFor(x => x.PostalCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(5)
                .NotEmpty();

            //update
            RuleFor(c => c.PostalCode).Matches("^\\d{5}$")
                //@"^\d{5}$")
                .When(c => c.Country == CountryEnum.MY)
                .WithMessage("Malaysian Postcodes have 5 digits");

        }
    }
}
