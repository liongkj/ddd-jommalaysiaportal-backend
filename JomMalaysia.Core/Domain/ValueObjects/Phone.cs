using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        private Phone() { }

        public string Number { get; private set; }
        public string AreaCode { get; private set; }
        public bool isLandLine { get; private set; }

        public static Phone For(string phoneString)
        {
            var phone = new Phone();
            try
            {
                var index = phoneString.IndexOf("-", StringComparison.Ordinal);
                phone.AreaCode = phoneString.Substring(0, index);
                phone.Number = phoneString.Substring(index + 1);
                //TODO
                //determine is landline
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Phone number");
            }

            return phone;
        }
        public static explicit operator Phone(string phoneString)
        {
            return For(phoneString);
        }

        public override string ToString()
        {
            return $"+{AreaCode}-{Number}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return AreaCode;
            yield return isLandLine;
        }


    }
}