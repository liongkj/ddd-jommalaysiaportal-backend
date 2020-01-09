using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;
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
                var CategoryQuery = await _categoryRepository.FindByIdAsync(message.CategoryId);
                if (!CategoryQuery.Success)
                {
                    outputPort.Handle(new ListingResponse(CategoryQuery.Errors, false, CategoryQuery.Message));
                    return false;
                }
                category = CategoryQuery.Data.CategoryPath;
            }

            var ListingQuery = await _listingRepository.GetAllListings(category, message.Type, message.GroupBySub, message.PublishStatus, message.SelectedDistrict);
            if (!ListingQuery.Success)
            {
                outputPort.Handle(new ListingResponse(ListingQuery.Errors, false, ListingQuery.Message));
                return false;
            }
            var vm = _mapper.Map<List<ListingViewModel>>(ListingQuery.Listings);
            foreach (var l in ListingQuery.Listings)
            {
                var cat = await _categoryRepository.FindByNameAsync(l.Category.Category, l.Category.Subcategory);
                vm.Where(x => x.ListingId == l.ListingId).FirstOrDefault().Category = cat;
            }

            outputPort.Handle(new ListingResponse(vm, true, ListingQuery.Message));
            return ListingQuery.Success;
        }
    }
}
