using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Exceptions;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.Validation;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Create
{
    public class CreateMerchantRequestValidator : AbstractValidator<CreateMerchantRequest>, IValidatorInterceptor
    {
        public CreateMerchantRequestValidator()
        {
            RuleFor(x => x.Address)
             .NotEmpty()
             .SetValidator(new AddressValidator());

            RuleFor(x => x.OldSsmId)
            .Must(ValidOldRegistrationNo)
            .When(x => !string.IsNullOrEmpty(x.OldSsmId))
            .WithMessage("{PropertyName} format is wrong characters. Sample Format (1312525-A)");


            RuleFor(x => x.SsmId)
            .NotEmpty()
            .NotNull()
            .Must(ValidNewRegistrationNo)
            .OnFailure((obj, context, errorMessage) => throw new BadRequestException(errorMessage))
            .WithMessage("{PropertyName} should have 12 digits.Sample Format (201901000005)");

            RuleFor(x => x.CompanyRegistrationName).NotEmpty().WithMessage("{PropertyName} must not be blank");
            RuleFor(x => x.Contacts.Count).GreaterThan(0).WithMessage("{PropertyName} shuould have at least one primary contact");

            RuleForEach(x => x.Contacts).SetValidator(new ContactValidator()).When(x => x.Contacts.Count > 0);
        }
        protected bool ValidOldRegistrationNo(string regNo)
        {
            if (regNo != null)
            {
                var regNumber = regNo.Trim().Split('-');
                if (regNumber.Length == 1) return false;
                var sufficientLength = regNumber[0].Length >= 6 && regNumber[0].Length <= 7; //check number 6 -7;
                var IsAlpha = char.IsLetter(char.Parse(regNumber[1]));
                return IsAlpha && sufficientLength;
            }
            return false;
        }

        protected bool ValidNewRegistrationNo(string regNo)
        {
            var regNumber = regNo.Trim();
            var IsDigit = regNumber.All(Char.IsDigit);
            var validLength = regNumber.Length == 12;
            //check number 6 -7;
            return IsDigit && validLength;

        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {

            if (!result.IsValid)
            {
                var failure = new ValidationFailure(result.Errors.FirstOrDefault().PropertyName, result.Errors.FirstOrDefault().ErrorMessage);
                var ValResult = new ValidationResult(new List<ValidationFailure> { failure });
                return ValResult;
            }
            return result;
        }
    }
}
