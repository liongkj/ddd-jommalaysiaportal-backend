using FluentValidation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish

{
    public class PublishListingRequestValidator : AbstractValidator<PublishListingRequest>
    {
        public PublishListingRequestValidator()
        {
            RuleFor(x => x.ListingId).NotNull().NotEmpty().Length(24).WithMessage("Please enter a valid {PropertyName}");
            RuleFor(x => x.Months).NotNull().NotEmpty().WithMessage("Please enter a valid month");

        }
    }
}