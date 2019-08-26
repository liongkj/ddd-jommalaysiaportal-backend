using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class CoordinatesValidator : AbstractValidator<Coordinates>
    {
        public CoordinatesValidator()
        {

            RuleFor(x => x.Latitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} is not valid. -90 > x > 90");
            RuleFor(x => x.Longitude)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidLongitude)
                .WithMessage("{PropertyName} is not valid. -180 > x > 180");
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