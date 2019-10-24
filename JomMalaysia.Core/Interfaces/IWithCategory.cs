using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Core.Interfaces
{
    public interface IWithCategory
    {
        Dictionary<string, string> updateCategory(Listing toBeUpdate, bool IsUpdateCategory = true);
    }
}
