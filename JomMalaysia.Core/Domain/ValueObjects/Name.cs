using System.Reflection.Metadata;
using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public sealed class Name : ValueObjectBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private Name() { }
        public static explicit operator Name(string nameString)
        {
            return For(nameString);
        }
        public static Name For(string nameString)
        {
            var name = new Name();
            try
            {
                var index = nameString.IndexOf(" ", StringComparison.Ordinal);
                name.FirstName = nameString.Substring(0, index);
                name.LastName = nameString.Substring(index + 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return name;
        }

        public Name(string FirstName, string LastName)
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new Exception("First name is invalid");
            if (string.IsNullOrWhiteSpace(LastName)) throw new Exception("Last name is invalid");

            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
