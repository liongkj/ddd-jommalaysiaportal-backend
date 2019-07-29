using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class ContactValidator: AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Email).SetValidator(new EmailValidator()); //check from EmailValidator
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator()); //check from PhoneValidator
            RuleFor(x => x.Name).SetValidator(new NameValidator()); //check from NameValidator

        }
    }

}
