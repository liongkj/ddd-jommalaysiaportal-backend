using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Interfaces
{
    public interface IWithCategory
    {
        CategoryPath Category { get; set; }
        string ListingId { get; set; }
    }
}
