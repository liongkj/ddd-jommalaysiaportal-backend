using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Delete
{
    public class DeleteListingUseCase : IDeleteListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        public DeleteListingUseCase(IListingRepository listing)
        {
            _listingRepository = listing;
        }

        public async Task<bool> Handle(DeleteListingRequest message, IOutputPort<DeleteListingResponse> outputPort)
        {
            //check if is publish
            var getListingResponse = await _listingRepository.FindById(message.ListingId).ConfigureAwait(false);
            if (!getListingResponse.Success) //try to fetch listing 
            {
                outputPort.Handle(new DeleteListingResponse(getListingResponse.Errors, false, getListingResponse.Message));
                return false;
            }
            var ToBeDeleted = getListingResponse.Listing;
            if (ToBeDeleted.IsSafeToDelete()) //check if it is safe to delete
            {
                var deleteListingResponse = await _listingRepository.Delete(message.ListingId).ConfigureAwait(false);
                outputPort.Handle(deleteListingResponse);
                return deleteListingResponse.Success;
            }

            else //NOT SAFE TO DELETE 
            {
                outputPort.Handle(new DeleteListingResponse(message.ListingId, false, "Listing is still published."));
                return false;
            }


        }
    }
}
