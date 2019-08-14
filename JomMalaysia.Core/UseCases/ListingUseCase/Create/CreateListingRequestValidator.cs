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
            //if listing type is event must have eventdate
            

            RuleFor(p => p.MerchantId).NotEmpty().NotNull();
        }
    }
}
