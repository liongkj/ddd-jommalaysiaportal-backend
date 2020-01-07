using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Update
{
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateMerchantUseCase(IMongoDbContext transaction, IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository; _transaction = transaction;
        }
        public async Task<bool> Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
        {
            using (var session = await _transaction.StartSession())
            {
                //verify update??
                var response = await _merchantRepository.UpdateMerchantAsyncWithSession(message.MerchantId, message.Updated, session);

                outputPort.Handle(response);
                return response.Success;
            }
        }


    }
}
