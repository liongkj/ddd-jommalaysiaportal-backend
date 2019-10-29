
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetListingUseCase : IGetListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;

        public GetListingUseCase(IListingRepository listingRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetListingRequest message, IOutputPort<GetListingResponse> outputPort)
        {
            if (message is null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            var response = await _listingRepository.FindById(message.Id).ConfigureAwait(false);
            if (!response.Success)
            {
                outputPort.Handle(new GetListingResponse(response.Errors));
            }
            if (response.Listing != null)
            {
                var listingVM = _mapper.Map<ListingViewModel>(response.Listing);
                outputPort.Handle(new GetListingResponse(listingVM, true));
                return response.Success;
            }
            else
            {
                outputPort.Handle(new GetListingResponse(response.Errors, false, "Listing Deleted or Not Found"));
                return false;
            }
        }
    }
}
