using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CompanyRegistration : ValueObjectBase
    {
        public string OldSsmId { get; private set; }
        public string SsmId { get; private set; }
        public string RegistrationName { get; private set; }

        private CompanyRegistration() { }


        public static CompanyRegistration For(string regString, string regName, string oldSsmId = null)
        {
            var regNo = new CompanyRegistration
            {
                SsmId = regString,
                RegistrationName = regName,
                OldSsmId = oldSsmId ?? oldSsmId
            };
            //todo Validation
            return regNo;
        }
        public override string ToString()
        {
            return SsmId;
        }

        public CompanyRegistration(string regNo, string regName, string oldSsmId = null)
        {
            SsmId = regNo;
            RegistrationName = regName;
            OldSsmId = oldSsmId;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}