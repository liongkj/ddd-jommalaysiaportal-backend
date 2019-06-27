using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
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
