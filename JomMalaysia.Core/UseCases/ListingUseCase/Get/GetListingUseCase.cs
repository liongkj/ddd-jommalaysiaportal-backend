
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetListingUseCase : IGetListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetListingUseCase(IListingRepository listingRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetListingRequest message, IOutputPort<GetListingResponse> outputPort)
        {


            var response = await _listingRepository.FindById(message.Id).ConfigureAwait(false);
            if (!response.Success)
            {
                outputPort.Handle(new GetListingResponse(response.Errors));
            }
            if (response.Listing != null)
            {
                var category = await _categoryRepository.FindByNameAsync(response.Listing.Category.Category, response.Listing.Category.Subcategory);

                var mapped = _mapper.Map<ListingViewModel>(response.Listing);
                mapped.Category = category;
                outputPort.Handle(new GetListingResponse(mapped, response.Success, response.Message));
                return response.Success;
            }

            outputPort.Handle(new GetListingResponse(response.Errors, false, "Listing Deleted or Not Found"));
            return false;
        }
    }
}
