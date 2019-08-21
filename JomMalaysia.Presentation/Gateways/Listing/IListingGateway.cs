using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Framework.WebServices;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Presentation.Gateways.Listing
{
    public interface IListingGateway
    {
        Task<List<ListingViewModel>> GetListings();
        Task<IWebServiceResponse> CreateListing(ListingViewModel vm);
    }
}
