using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Contact:ValueObject
    {

        private Contact()
        {

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
        public Name ContactName { get; private set; }
        public Email ContactEmail { get; private set; }
        public Phone ContactPhone { get; private set; }
    }
}
