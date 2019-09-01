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

            RuleFor(x => x.CompanyName).NotEmpty();

            RuleFor(x => x.CompanyRegistrationNumber).NotEmpty().SetValidator(new CompanyRegNumValidator());

            RuleFor(x => x.Contacts.Count).GreaterThan(0);
            // public string CompanyName { get; }
            //         public CompanyRegistrationNumber CompanyRegistrationNumber { get; }
            //         public Address Address { get; }
            //         public IReadOnlyCollection<Contact> Contacts { get; }
            //         public ICollection<Listing> Listings { get; }
        }
    }
}
