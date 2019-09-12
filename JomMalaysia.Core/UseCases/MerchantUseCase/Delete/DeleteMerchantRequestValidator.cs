using FluentValidation;
using JomMalaysia.Core.UseCases.MerchantUseCase.Delete;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Delete
{
    public class DeleteMerchantRequestValidator : AbstractValidator<DeleteMerchantRequest>
    {
        public DeleteMerchantRequestValidator()
        {
            RuleFor(x => x.MerchantId)
             .NotEmpty()
             .WithMessage("{PropertyName} should not be empty");
        }

    }
}
