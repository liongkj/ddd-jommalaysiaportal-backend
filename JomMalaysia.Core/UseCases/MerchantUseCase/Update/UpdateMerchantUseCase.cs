using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Update
{
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public UpdateMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public async Task<bool> Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
        {
            if (message is null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }
            //TODO
            //verify update??
            var response = await _merchantRepository.UpdateMerchant(message.MerchantId, message.Updated);

            outputPort.Handle(response.Success ? new UpdateMerchantResponse(response.Id, true) : new UpdateMerchantResponse(response.Errors));
            return response.Success;
        }


    }
}
