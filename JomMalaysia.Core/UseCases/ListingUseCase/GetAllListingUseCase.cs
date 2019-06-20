using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases.Listings;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class GetAllListingUseCase : IGetAllListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public GetAllListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public async Task<bool> Handle(GetAllListingRequest message, IOutputPort<GetAllListingResponse> outputPort)
        {
            var response = await _listingRepository.GetAllListings();
            if (!response.Success)
            {
                outputPort.Handle(new GetAllListingResponse(response.Errors));
            }
            outputPort.Handle(new GetAllListingResponse(response.Listings, true));

            return response.Success;
            //throw new NotImplementedException();
            //TODO 

        }
    }
}
