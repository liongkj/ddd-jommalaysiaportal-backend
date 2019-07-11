using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class ContactValidatorcs: AbstractValidator<Contact>
    {
        public ContactValidatorcs()
        {
            RuleFor(x => x.Email).SetValidator(new EmailValidator());
        }
    }

}
