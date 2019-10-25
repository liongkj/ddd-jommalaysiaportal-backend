using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public class UpdateListingUseCase : IUpdateListingUseCase
    {
        #region Dependencies
        private readonly IListingRepository _listingRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMongoDbContext _transaction;
        private readonly IUpdatePublishListingUseCase _updatePublishedListingUseCase;

        public UpdateListingUseCase(IListingRepository listingRepository
        , IMerchantRepository merchantRepository,
        ICategoryRepository categoryRepository,
        IWorkflowRepository workflowRepository,
        IUpdatePublishListingUseCase updatePublishListingUseCase,
        IMongoDbContext transaction
        )
        {
            _workflowRepository = workflowRepository;
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _updatePublishedListingUseCase = updatePublishListingUseCase;
            _transaction = transaction;
        }
        #endregion
        public async Task<bool> Handle(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort)
        {

            var OldListingResponse = await _listingRepository.FindById(message.ListingId);
            if (!OldListingResponse.Success)
            {
                outputPort.Handle(new CoreListingResponse(OldListingResponse.Errors, false, OldListingResponse.Message));
                return false;
            }
            var OldListing = OldListingResponse.Listing;

            if (!OldListing.IsPublished())
            {//hanldle unpublished listing
                return await UpdateOperation.HandleListingUpdate(message, outputPort, _categoryRepository, _transaction, _merchantRepository, _listingRepository);
            }
            else
            {//handle published listing
                return await _updatePublishedListingUseCase.Handle(message, outputPort);
            }
        }


    }
}
