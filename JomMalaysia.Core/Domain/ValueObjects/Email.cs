using FluentValidation;
using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Email : ValueObjectBase, IEquatable<Email>, IEquatable<string>
    {
        private Email() { }

        public string User { get; private set; }
        public string Domain { get; private set; }
        public static Email For(string emailstring)
        {
            var email = new Email();

            try
            {
                var index = emailstring.IndexOf('@', StringComparison.Ordinal);
                if (index >= 0)
                {
                    email.User = emailstring.Substring(0, index);
                    email.Domain = emailstring.Substring(index + 1);
                }
                else throw new ValidationException("Email format is incorrect");
            }
            catch (Exception e)
            {
                throw new ValidationException(e.ToString());
            }
            return email;
        }

        public static explicit operator Email(string emailString)
        {
            return For(emailString);
        }

        public override string ToString()
        {
            return $"{User}@{Domain}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return User;
            yield return Domain;
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Email other)
        {
            throw new NotImplementedException();
        }
    }
}