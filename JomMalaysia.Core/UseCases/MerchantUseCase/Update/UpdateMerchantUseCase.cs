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
        public bool Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
        {
            //TODO
            //verify update??
            var response = _merchantRepository.Update(message.MerchantId,message.Updated);

            outputPort.Handle(response.Success ? new UpdateMerchantResponse(response.Id, true) : new UpdateMerchantResponse(response.Errors));
            return response.Success;
        }

       
    }
}
