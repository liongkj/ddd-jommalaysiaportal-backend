using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JomMalaysia.Api.UseCases.Merchants
{
    public class MerchantViewModelValidator : AbstractValidator<MerchantViewModel>
    {
        public MerchantViewModelValidator()
        {
            RuleFor(x => x.CompanyName).Length(3, 30);
        }
    }
}
