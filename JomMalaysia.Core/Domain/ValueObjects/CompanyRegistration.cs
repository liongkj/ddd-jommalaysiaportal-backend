using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CompanyRegistration : ValueObjectBase
    {
        public string SsmId { get; private set; }
        public string RegistrationName { get; private set; }

        private CompanyRegistration() { }


        public static CompanyRegistration For(string regString, string regName)
        {
            var regNo = new CompanyRegistration
            {
                SsmId = regString,
                RegistrationName = regName
            };
            //todo Validation
            return regNo;
        }
        public override string ToString()
        {
            return SsmId;
        }

        public CompanyRegistration(string regNo, string regName)
        {
            SsmId = regNo;
            RegistrationName = regName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}