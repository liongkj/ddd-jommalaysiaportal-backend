using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CompanyRegistrationNumber : ValueObjectBase
    {
        public string RegistrationNumber { get; private set; }

        private CompanyRegistrationNumber() { }
        public static explicit operator CompanyRegistrationNumber(string nameString)
        {
            return For(nameString);
        }

        public static CompanyRegistrationNumber For(string regString)
        {
            var regNo = new CompanyRegistrationNumber
            {
                RegistrationNumber = regString
            };
            //todo Validation
            return regNo;
        }

        public CompanyRegistrationNumber(string regNo)
        {
            if (string.IsNullOrWhiteSpace(regNo)) throw new Exception("Company registration number is invalid");
            

            this.RegistrationNumber = regNo;

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}