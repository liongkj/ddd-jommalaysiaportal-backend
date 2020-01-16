using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.MobileUseCases.SearchListings
{
    public class SearchListingUseCase : ISearchListingUseCase
    {
        IMapper _mapper;
        
        private readonly ICategoryRepository _categoryRepository;

        private readonly IGeospatialRepository _geospatialQuery;

        public SearchListingUseCase(IMapper mapper, IGeospatialRepository geospatialQuery,ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _geospatialQuery = geospatialQuery;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(SearchListingRequest message, IOutputPort<ListingResponse> outputPort)
        {

            var SearchListingQuery = await _geospatialQuery.ListingSearch(message.q, message.lang);
            var vm = _mapper.Map<List<ListingViewModel>>(SearchListingQuery.Listings);
            foreach (var l in SearchListingQuery.Listings)
            {
                var cat = await _categoryRepository.FindByNameAsync(l.Category.Category, l.Category.Subcategory);
                if (vm != null) vm.FirstOrDefault(x => x.ListingId == l.ListingId).Category = cat;
            }
            outputPort.Handle(new ListingResponse(vm, true, SearchListingQuery.Message));
            return SearchListingQuery.Success;
        }
    }
}
