using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

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


            var ToBeDeleted = getListingResponse.Listing;
            var getMerchantResponse = await _merchantRepository.FindByIdAsync(ToBeDeleted.Merchant.MerchantId).ConfigureAwait(false);
            var Merchant = getMerchantResponse.Merchant;
            if (ToBeDeleted.IsSafeToDelete()) //check if it is safe to delete
            {
                Merchant.RemoveListing(ToBeDeleted);
                using (var session = await _transaction.StartSession())
                {
                    try
                    {
                        session.StartTransaction();
                        var UpdateMerchantResponse = await _merchantRepository.UpdateMerchantAsyncWithSession(Merchant.MerchantId, Merchant, session).ConfigureAwait(false);
                        var deleteListingResponse = await _listingRepository.DeleteAsyncWithSession(message.ListingId, session).ConfigureAwait(false);
                        if (deleteListingResponse.Success)
                        {
                            await session.CommitTransactionAsync();
                            outputPort.Handle(deleteListingResponse);
                            return deleteListingResponse.Success;
                        }
                        else
                        {
                            outputPort.Handle(new DeleteListingResponse(deleteListingResponse.Errors, deleteListingResponse.Success, deleteListingResponse.Message));
                            return false;
                        }
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

            else //NOT SAFE TO DELETE 
            {
                outputPort.Handle(new DeleteListingResponse(message.ListingId, false, "Listing is still published."));
                return false;
            }


        }
    }
}
