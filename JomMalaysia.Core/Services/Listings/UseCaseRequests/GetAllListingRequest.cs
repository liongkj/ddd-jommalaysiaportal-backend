using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Services.Listings.UseCaseRequests
{
    public class GetAllListingRequest:IUseCaseRequest<GetAllListingResponse>
    {
       public ICollection<Listing> Listings { get; set; }
        public string Id { get; set; }
        public GetAllListingRequest()
        {
            Listings = new Collection<Listing>();
        }
    }
}
