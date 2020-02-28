using System;
using System.Collections.Generic;
using PhoneNumbers;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Phone : ValueObjectBase
    {

        private Phone() { }

        public string Number { get; set; }


        public static Phone For(string phoneString)
        {
            var phone = new Phone();
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                const string countryCode = "MY";

                var parsedPhone = phoneUtil.Parse(phoneString, countryCode);

                phone.Number = phoneUtil.FormatNumberForMobileDialing(parsedPhone,countryCode,true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return phone;
        }

        public override string ToString()
        {
            return Number;
        }
        public static explicit operator Phone(string phoneString)
        {
            if (phoneString != null)
                return For(phoneString);
            return null;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;

        }


    }
}