using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public class UpdateListingUseCase : IUpdateListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public UpdateListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public async Task<bool> Handle(UpdateListingRequest message, IOutputPort<UpdateListingResponse> outputPort)
        {
            //TODO
            //update listing
            var updateListingResponse = await _listingRepository.UpdateAsyncWithSession(message.Updated);

            outputPort.Handle(updateListingResponse);
            return updateListingResponse.Success;
        }


    }
}
