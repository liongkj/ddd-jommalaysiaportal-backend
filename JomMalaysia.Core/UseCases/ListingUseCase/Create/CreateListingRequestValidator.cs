using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingRequestValidator : AbstractValidator<CreateListingRequest>
    {
        public CreateListingRequestValidator()
        {
            RuleFor(x => x.Address);
            // .NotEmpty()
            // .SetValidator(new AddressValidator());
            RuleForEach(x => x.Coordinates).NotNull();
            RuleFor(l => l.ListingType).NotEmpty();
            RuleFor(l => l.Category).NotEmpty().NotNull();
            RuleFor(l => l.Subcategory).NotEmpty().NotNull();

            //if listing type is event must have eventdate
            RuleFor(req => req.EventStartDateTime).NotEmpty().NotEmpty().When(m => m.ListingType.Equals(ListingTypeEnum.Event.ToString())).WithMessage("Please enter a valid start date for event type listing");
            RuleFor(req => req.EventEndDateTime).NotEmpty().NotEmpty().When(m => m.ListingType.Equals(ListingTypeEnum.Event.ToString())).WithMessage("Please enter a valid end date for event type listing"); ;


            RuleFor(p => p.MerchantId).NotEmpty().NotNull().WithMessage("Please select a valid merchant");
        }
    }
}
