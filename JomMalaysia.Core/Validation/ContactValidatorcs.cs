using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class ContactValidatorcs: AbstractValidator<Contact>
    {
        public ContactValidatorcs()
        {
            RuleFor(x => x.Email).SetValidator(new EmailValidator()); //check from EmailValidator
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator()); //check from PhoneValidator
            RuleFor(x => x.Name).SetValidator(new NameValidator()); //check from NameValidator

        }
    }

}
