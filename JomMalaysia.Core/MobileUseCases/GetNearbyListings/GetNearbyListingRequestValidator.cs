
using FluentValidation;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.MobileUseCase.GetNearbyListings
{
    public class GetNearbyListingRequestValidator : AbstractValidator<GetNearbyListingRequest>
    {
        public GetNearbyListingRequestValidator()
        {
            RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("{PropertyName} must not be Empty")
            .SetValidator(new CoordinatesValidator());


        }
    }
}