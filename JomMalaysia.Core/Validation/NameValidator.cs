﻿using System;
using System.Linq;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50)
                .Must(BeAValidName).WithMessage("{PropertyName} is invalid.");
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(2,30)
                .Must(BeAValidName).WithMessage("{PropertyName} is invalid.");

        }

        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            name = name.Replace(".", "");
            return name.All(Char.IsLetter);
        }
    }
}
