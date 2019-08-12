using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingRequestValidator : AbstractValidator<CreateListingRequest>
    {
        public CreateListingRequestValidator()
        {
            RuleFor(CreateListingRequest => CreateListingRequest.Address).SetValidator(new AddressValidator());
            

            RuleFor(p => p.MerchantId).NotEmpty().NotNull();
        }
    }
}
