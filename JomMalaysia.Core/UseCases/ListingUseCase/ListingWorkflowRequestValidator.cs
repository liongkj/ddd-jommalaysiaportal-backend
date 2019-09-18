using FluentValidation;

namespace JomMalaysia.Core.UseCases.ListingUseCase

{
    public class ListingWorkflowRequestValidator : AbstractValidator<ListingWorkflowRequest>
    {
        public ListingWorkflowRequestValidator()
        {
            RuleFor(x => x.ListingId).NotNull().NotEmpty().Length(24).WithMessage("Please enter a valid {PropertyName}");

        }
    }
}