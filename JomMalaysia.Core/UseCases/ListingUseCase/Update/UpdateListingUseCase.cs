using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public class UpdateListingUseCase : IUpdateListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public UpdateListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public async Task<bool> Handle(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort)
        {
            //TODO add factory
            //update listing
            CoreListingResponse updateListingResponse = null; // ==await _listingRepository.UpdateAsyncWithSession(message.Updated);

            outputPort.Handle(updateListingResponse);
            return updateListingResponse.Success;
        }


    }
}
