using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Delete
{
    public class DeleteListingUseCase : IDeleteListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMongoDbContext _transaction;
        public DeleteListingUseCase(IListingRepository listing, IMerchantRepository merchant, IMongoDbContext transaction)
        {
            _listingRepository = listing;
            _transaction = transaction;
            _merchantRepository = merchant;
        }

        public async Task<bool> Handle(DeleteListingRequest message, IOutputPort<DeleteListingResponse> outputPort)
        {
            //check if is publish
            var getListingResponse = await _listingRepository.FindById(message.ListingId).ConfigureAwait(false);
            if (!getListingResponse.Success) //try to fetch listing 
            {
                outputPort.Handle(new DeleteListingResponse(getListingResponse.Errors, false, getListingResponse.Message));
                return false;
            }


            var toBeDeleted = getListingResponse.Listing;
            var getMerchantResponse = await _merchantRepository.FindByIdAsync(toBeDeleted.Merchant.MerchantId).ConfigureAwait(false);
            var merchant = getMerchantResponse.Merchant;
            if (toBeDeleted.IsSafeToDelete()) //check if it is safe to delete
            {
                merchant.RemoveListing(toBeDeleted);
                using (var session = await _transaction.StartSession())
                {
                    try
                    {
                        session.StartTransaction();
                        var updateMerchantResponse = await _merchantRepository.UpdateMerchantAsyncWithSession(merchant.MerchantId, merchant, session).ConfigureAwait(false);
                        var deleteListingResponse = await _listingRepository.DeleteAsyncWithSession(message.ListingId, session).ConfigureAwait(false);
                        if (deleteListingResponse.Success)
                        {
                            await session.CommitTransactionAsync();
                            outputPort.Handle(deleteListingResponse);
                            return deleteListingResponse.Success;
                        }

                        outputPort.Handle(new DeleteListingResponse(deleteListingResponse.Errors, deleteListingResponse.Success, deleteListingResponse.Message));
                        return false;
                    }
                    catch (Exception e)
                    {
                        outputPort.Handle(new DeleteListingResponse(
                            $"{GetType().Name} Transaction Error",
                            false,
                             e.ToString()));
                        return false;
                    }
                }

            }

            outputPort.Handle(new DeleteListingResponse(message.ListingId, false, "Listing is still published."));
            return false;


        }
    }
}
