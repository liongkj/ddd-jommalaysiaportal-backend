using System;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases;
using JomMalaysia.Core.Services.UseCaseRequests;

namespace JomMalaysia.Core.UseCases.MerchantUseCase
{
    public class DeleteMerchantUseCase : IDeleteMerchantUseCase
    {
        private readonly IMerchantRepository _merchant;
        public DeleteMerchantUseCase(IMerchantRepository merchant)
        {
            _merchant = merchant;
        }

        public bool Handle(DeleteMerchantRequest message, IOutputPort<DeleteMerchantResponse> outputPort)
        {
            if (message.Listings.Count > 0)
            {
                //still have listing, cannot delete
                outputPort.Handle(new DeleteMerchantResponse("Merchant still has listing associated"));
            }
            var response = _merchant.Delete(message.MerchantId);
            outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, true));
            return response.Success;
        }
    }
}