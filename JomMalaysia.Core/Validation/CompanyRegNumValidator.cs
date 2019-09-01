using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Validation.Extension;

namespace JomMalaysia.Core.Validation
{
    public class CompanyRegNumValidator : AbstractValidator<CompanyRegistrationNumber>
    {
        public CompanyRegNumValidator()
        {
            RuleFor(x => x.RegistrationNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NoStartWithWhiteSpace();
            //TODO find malaysia company reg pattern

        }
    }
}
