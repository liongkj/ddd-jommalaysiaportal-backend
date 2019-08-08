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
            RuleFor(CreateListingRequest => CreateListingRequest.ListingLocation).SetValidator(new LocationValidator());
        }
    }
}
