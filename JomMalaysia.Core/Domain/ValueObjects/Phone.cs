using System;
using System.Collections.Generic;
using com.google.i18n.phonenumbers.geocoding;
using libphonenumber;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Phone : ValueObjectBase
    {
        //TODO Use google phone library
        private Phone() { }

        public string Number { get; set; }


        public static Phone For(string phoneString)
        {
            var phone = new Phone();
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.Instance;

            try
            {
                string countryCode = "MY";

                PhoneNumber parsedPhone = phoneUtil.Parse(phoneString, countryCode);

                phone.Number = parsedPhone.Format(PhoneNumberUtil.PhoneNumberFormat.RFC3966);

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