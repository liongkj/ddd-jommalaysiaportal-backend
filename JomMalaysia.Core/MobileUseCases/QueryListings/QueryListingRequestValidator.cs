
using FluentValidation;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.MobileUseCase.QueryListings
{
    public class GetNearbyListingRequestValidator : AbstractValidator<GetNearbyListingRequest>
    {
        public GetNearbyListingRequestValidator()
        {

        }
    }
}