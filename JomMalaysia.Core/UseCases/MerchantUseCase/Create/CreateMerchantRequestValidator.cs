using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateMerchantRequestValidator : AbstractValidator<CreateMerchantRequest>
    {
        public CreateMerchantRequestValidator()
        {
            RuleFor(x => x.Address)
             .NotEmpty()
             .SetValidator(new AddressValidator());

            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("{PropertyName} should not be empty");

            RuleFor(x => x.CompanyRegistrationNumber).NotEmpty().Must(BeValidCompanyReqNo);

            RuleFor(x => x.Contacts.Count).GreaterThan(0).WithMessage("{PropertyName} shuould have at least one primary contact");

            RuleForEach(x => x.Contacts).SetValidator(new ContactValidator());
        }
        protected bool BeValidCompanyReqNo(string regNo)
        {
            return true;
            //TODO Check malaysia company reg pattern
        }

    }
}
