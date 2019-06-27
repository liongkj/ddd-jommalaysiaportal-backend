using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Factory
{
    abstract class Factory
    {
        public abstract Listing GetListing(string type);

    }
}