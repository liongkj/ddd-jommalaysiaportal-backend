using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.MobileUseCases.QueryListings
{
    public class QueryListingUseCase : IQueryListingUseCase
    {
        IListingRepository _listingRepository;
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public QueryListingUseCase(IListingRepository listingRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(QueryListingRequest message, IOutputPort<ListingResponse> outputPort)
        {
            CategoryPath category = null;
            if (message.CategoryId != null)
            {
                var categoryQuery = await _categoryRepository.FindByIdAsync(message.CategoryId);
                if (!categoryQuery.Success)
                {
                    outputPort.Handle(new ListingResponse(categoryQuery.Errors, false, categoryQuery.Message));
                    return false;
                }
                category = categoryQuery.Data.CategoryPath;
            }

            var listingQuery = await _listingRepository.GetAllListings(category, message.Type, message.GroupBySub, message.PublishStatus, message.SelectedCity, message.Featured);
            if (!listingQuery.Success)
            {
                outputPort.Handle(new ListingResponse(listingQuery.Errors, false, listingQuery.Message));
                return false;
            }
            var vm = _mapper.Map<List<ListingViewModel>>(listingQuery.Listings);
            foreach (var l in listingQuery.Listings)
            {
                var cat = await _categoryRepository.FindByNameAsync(l.Category.Category, l.Category.Subcategory);
                if (vm != null) vm.FirstOrDefault(x => x.ListingId == l.ListingId).Category = cat;
            }

            outputPort.Handle(new ListingResponse(vm, true, listingQuery.Message));
            return listingQuery.Success;
        }
    }
}
