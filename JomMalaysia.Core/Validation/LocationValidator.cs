using System;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
namespace JomMalaysia.Core.Validation
{
    public class LocationValidator: AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(x => x.Address).SetValidator(new AddressValidator());
            RuleFor(x => x.Latitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
            RuleFor(x => x.Longitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidLongitude);
        }

        protected bool BeAValidLatitude(double latitude)
        {
            return (latitude < -90.00 || latitude > 90.00);
        }

        protected bool BeAValidLongitude(double longitude)
        {
            return (longitude < -180.00 || longitude > 180.00);
        }
    }
}
