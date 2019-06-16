using System;
using System.Collections.Generic;
using com.google.i18n.phonenumbers.geocoding;
using libphonenumber;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        //TODO Use google phone library
        private Phone() { }

        public string Number { get; private set; }
        public string Area { get; private set; }
        public bool isMobile { get; private set; }

        public static Phone For(string phoneString)
        {
            var phone = new Phone();
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.Instance;

            try
            {
                string countryCode = "MY";
                PhoneNumber parsedPhone = phoneUtil.Parse(phoneString, countryCode);
                if (parsedPhone.NumberType.Equals(PhoneNumberUtil.PhoneNumberType.MOBILE))
                {
                    phone.isMobile = true;

                }
                else
                {
                    phone.isMobile = false;
                    //TODO get area based on phone 
                    //06 = Seremban
                    //03 = KL
                }

                phone.Number = parsedPhone.Format(PhoneNumberUtil.PhoneNumberFormat.INTERNATIONAL);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return phone;
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