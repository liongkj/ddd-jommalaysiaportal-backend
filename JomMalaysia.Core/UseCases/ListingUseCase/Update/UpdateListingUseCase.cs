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
