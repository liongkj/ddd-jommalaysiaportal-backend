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

namespace JomMalaysia.Core.MobileUseCases.QueryListings
{
    public class QueryListingUseCase : IQueryListingUseCase
    {
        IListingRepository _listingRepository;
        ICategoryRepository _categoryRepository;

        public QueryListingUseCase(IListingRepository listingRepository, ICategoryRepository categoryRepository)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
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

            var ListingQuery = await _listingRepository.GetAllListings(category, message.Type, message.GroupBySub, message.PublishStatus);
            if (!ListingQuery.Success)
            {
                outputPort.Handle(new ListingResponse(ListingQuery.Errors, false, ListingQuery.Message));
                return false;
            }
            outputPort.Handle(new ListingResponse(ListingQuery.Listings, true, ListingQuery.Message));
            return ListingQuery.Success;
        }
    }
}
