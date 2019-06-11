using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JomMalaysia.Api.UseCases.Merchants
{
    public class MerchantModelValidator:AbstractValidator<MerchantModel>
    {
        public MerchantModelValidator()
        {
            RuleFor(x => x.CompanyName).Length(3, 30);
        }
    }
}
