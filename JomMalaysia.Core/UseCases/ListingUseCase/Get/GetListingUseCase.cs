
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetListingUseCase : IGetListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public GetListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public bool Handle(GetListingRequest message, IOutputPort<GetListingResponse> outputPort)
        {
            var response = _listingRepository.FindById(message.Id);
            if (!response.Success)
            {
                outputPort.Handle(new GetListingResponse(response.Errors));
            }
            if (response.Listing != null)
            {
                outputPort.Handle(new GetListingResponse(response.Listing, true));
                return response.Success;
            }
            else
            {
                outputPort.Handle(new GetListingResponse(response.Errors,false,"Listing Deleted or Not Found"));
                return false;
            }
        }
    }
}
