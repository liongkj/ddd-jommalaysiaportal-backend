using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public class UpdatePublishedListingUseCase : IUpdatePublishListingUseCase
    {
        #region Dependencies
        private readonly IListingRepository _listingRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMongoDbContext _transaction;

        private readonly ILoginInfoProvider _loginInfo;

        public UpdatePublishedListingUseCase(IListingRepository listingRepository
        , IMerchantRepository merchantRepository,
        ICategoryRepository categoryRepository,
        IMongoDbContext transaction,
        ILoginInfoProvider loginInfo
        )
        {
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _transaction = transaction;
            _loginInfo = loginInfo;
        }
        #endregion
        public async Task<bool> Handle(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort, Listing oldListing)
        {
            var AppUser = _loginInfo.AuthenticatedUser();
            if (AppUser != null && AppUser.CanUpdateLiveListing())
                return await UpdateOperation.HandleListingUpdate(message, outputPort, _categoryRepository, _transaction, _merchantRepository, _listingRepository, oldListing);
            outputPort.Handle(new CoreListingResponse(new List<string> { "Unauthorized" }, false, "You are not authorized to update a live listing"));
            return false;

        }


    }
}