﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
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
                category = CategoryQuery.Category.CategoryPath;
            }

            var ListingQuery = await _listingRepository.GetAllListings(category, message.Type, message.GroupBySub);
            if (!ListingQuery.Success)
            {
                outputPort.Handle(new ListingResponse(ListingQuery.Errors, false, ListingQuery.Message));
                return false;
            }
            outputPort.Handle(new ListingResponse(ListingQuery.Data, true, ListingQuery.Message));
            return ListingQuery.Success;
        }
    }
}