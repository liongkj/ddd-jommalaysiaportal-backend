using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
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
