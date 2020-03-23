using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.Indexes
{
    public class BatchIndexPlacesUseCase:IBatchIndexPlacesUseCase
    { 
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IListingIndexer _listingIndexer;

        public BatchIndexPlacesUseCase(IListingIndexer listingIndexer, IListingRepository listingRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _listingIndexer = listingIndexer;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AlgoliaIndexRequest message, IOutputPort<AlgoliaIndexResponse> outputPort)
        {
            AlgoliaIndexResponse response;
            List<ListingViewModel> listingVM;
            try
            {

                var getAllListingResponse = await _listingRepository.GetAllListings(null, false);
                listingVM = _mapper.Map<List<ListingViewModel>>(getAllListingResponse.Listings);
                foreach (var l in getAllListingResponse.Listings)
                {
                    var category = await _categoryRepository.FindByNameAsync(l.Category.Category, l.Category.Subcategory);
                    listingVM.FirstOrDefault(x => x.ListingId == l.ListingId).Category = category;
                }
                var result = await _listingIndexer.SaveObject(listingVM);
                response =  new AlgoliaIndexResponse(result,true); 
            }

            catch (Exception e)
            {
                response = new AlgoliaIndexResponse(new List<string> {e.Message});
            }

            outputPort.Handle(response);

            return response.Success;
        }
    }
}