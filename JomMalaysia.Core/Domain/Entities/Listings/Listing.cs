

using System;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Entities
{
    public class Listing
    {
        public Guid Id { get; private set; }
        public Merchant Merchant { get; private set; }
        public Address Address { get; private set; }
        public Listing()
        {
           
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
       
    }
}
