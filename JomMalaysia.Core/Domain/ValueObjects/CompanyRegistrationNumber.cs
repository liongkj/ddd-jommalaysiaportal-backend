using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CompanyRegistrationNumber : ValueObjectBase
    {
        public string RegistrationNumber { get; private set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}