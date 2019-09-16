using FluentValidation;

namespace JomMalaysia.Core.UseCases.ListingUseCase

{
    public class PublishListingRequestValidator : AbstractValidator<ListingWorkflowRequest>
    {
        public PublishListingRequestValidator()
        {
            RuleFor(x => x.ListingId).NotNull().NotEmpty().Length(24).WithMessage("Please enter a valid {PropertyName}");

        }
    }
}