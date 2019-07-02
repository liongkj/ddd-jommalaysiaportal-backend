using System;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Delete
{
    public class DeleteMerchantUseCase : IDeleteMerchantUseCase
    {
        private readonly IMerchantRepository _merchant;
        public DeleteMerchantUseCase(IMerchantRepository merchant)
        {
            _merchant = merchant;
        }

        public bool HandleAsync(DeleteMerchantRequest message, IOutputPort<DeleteMerchantResponse> outputPort)
        {
            if (message.MerchantId == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Merchant merchant = (_merchant.FindById(message.MerchantId)).Merchant;
            if (merchant == null)
            {
                outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, false, "Merchant Not Found"));
                return false;
            }
            else
            {
                if (merchant.isSafeToDelete())
                {
                    //still have listing, cannot delete
                    outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, false, "Merchant still has listing associated"));
                    return false;
                }
                var response = _merchant.DeleteMerchant(message.MerchantId);
                outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, true, merchant.MerchantId + " deleted"));
                return response.Success;
            }
        }
    }
}