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
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            //check if is publish
            var listing = (await _listingRepository.FindById(message.ListingId).ConfigureAwait(false)).Listing;
            //is safe to delete
            if (listing != null && listing.IsSafeToDelete())
            {
                var response = await _listingRepository.Delete(message.ListingId).ConfigureAwait(false);
                if (!response.Success)
                {
                    outputPort.Handle(new DeleteListingResponse(response.Errors));
                }
                if (response.Success)
                {
                    outputPort.Handle(new DeleteListingResponse(message.ListingId, true, "deleted successfully"));
                    return response.Success;
                }
                else
                {
                    outputPort.Handle(new DeleteListingResponse(response.Errors, false, "Listing Not Found"));
                    return false;
                }
            }
            //isPublish -> stop
            else
            {
                outputPort.Handle(new DeleteListingResponse(message.ListingId, false, "Listing is still published."));
                return false;
            }
            
        }
    }
}