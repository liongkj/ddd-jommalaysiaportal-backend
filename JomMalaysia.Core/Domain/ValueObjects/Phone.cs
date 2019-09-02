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
        public string Area { get; set; }
        public bool isMobile { get; set; }

        public static Phone For(string phoneString)
        {
            var phone = new Phone();
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.Instance;

            try
            {
                string countryCode = "MY";
                phone.isMobile = false;
                PhoneNumber parsedPhone = phoneUtil.Parse(phoneString, countryCode);
                if (parsedPhone.NumberType.Equals(PhoneNumberUtil.PhoneNumberType.MOBILE))
                {
                    phone.isMobile = true;

                }


                phone.Number = parsedPhone.Format(PhoneNumberUtil.PhoneNumberFormat.INTERNATIONAL);

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
            return For(phoneString);
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return Area;
            yield return isMobile;
        }


    }
}