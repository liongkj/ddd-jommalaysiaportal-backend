using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces
{
    public interface IListingRepository
    {
        CreateListingResponse CreateListing(Listing listing);
        Task<GetAllListingResponse> GetAllListings();
        DeleteListingResponse Delete(string id);
        GetListingResponse FindByName(string name);
        GetListingResponse FindById(string id);
        UpdateListingResponse Update(string id, Listing listing);
        
    }
}
