using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class CoreListingRequestValidator : AbstractValidator<CoreListingRequest>
    {
        public CoreListingRequestValidator()
        {

            RuleFor(x => x.MerchantId)
                .Length(24)
                .NotNull()
                .NotEmpty()
                .WithMessage("Merchant ID is not valid");
            RuleFor(x => x.Address);
            // .NotEmpty()
            // .SetValidator(new AddressValidator());
            RuleFor(l => l.ListingType).NotEmpty();
            RuleFor(l => l.CategoryId).NotEmpty().NotNull()
                .WithMessage("Please select a valid category for this listing type");

            //if listing type is event must have eventdate
//            RuleFor(req => req.EventStartDateTime).NotEmpty().NotNull().When(m => m.ListingType.Equals(ListingTypeEnum.Event.ToString())).WithMessage("Please enter a valid start date for event type listing");
//            RuleFor(req => req.EventEndDateTime).NotEmpty().NotNull().When(m => m.ListingType.Equals(ListingTypeEnum.Event.ToString())).WithMessage("Please enter a valid end date for event type listing"); ;


            RuleFor(p => p.MerchantId).NotEmpty().NotNull().WithMessage("Please select a valid merchant");

        }
       

    }
}
