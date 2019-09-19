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
            //TODO
            //verify update??
            var response = await _merchantRepository.UpdateMerchantAsyncWithSession(message.MerchantId, message.Updated);

            outputPort.Handle(response);
            return response.Success;
        }


    }
}
