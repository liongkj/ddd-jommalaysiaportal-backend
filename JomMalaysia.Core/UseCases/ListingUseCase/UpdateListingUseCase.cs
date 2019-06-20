using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases.Listings;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class UpdateListingUseCase : IUpdateListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public UpdateListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public bool Handle(UpdateListingRequest message, IOutputPort<UpdateListingResponse> outputPort)
        {
            //TODO
            //verify update??
            var response = _listingRepository.Update(message.ListingId,message.Updated);

            outputPort.Handle(response.Success ? new UpdateListingResponse(response.Id, true) : new UpdateListingResponse(response.Errors));
            return response.Success;
        }

       
    }
}
