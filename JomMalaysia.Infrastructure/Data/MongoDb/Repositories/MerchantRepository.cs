using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.UseCases.MerchantUseCase.Delete;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly IMongoCollection<MerchantDto> _db;
        private readonly IMapper _mapper;
        public MerchantRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<MerchantDto>("Merchant");
            _mapper = mapper;
        }
        public async Task<CreateMerchantResponse> CreateMerchant(Merchant merchant)
        {
            var merchantDto = _mapper.Map<Merchant, MerchantDto>(merchant);
            await _db.InsertOneAsync(merchantDto);
            return new CreateMerchantResponse(merchant.MerchantId, true);
        }

        public DeleteMerchantResponse DeleteMerchant(string merchantId)
        {
            var result = _db.DeleteOne(filter: m => m.Id == merchantId);
            //todo
            return new DeleteMerchantResponse(merchantId, true);
        }

        public GetMerchantResponse FindById(string merchantId)
        {
            throw new System.NotImplementedException();
        }

        public GetMerchantResponse FindByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetAllMerchantResponse> GetAllMerchants()
        {
            throw new System.NotImplementedException();
        }

        public Task<UpdateMerchantResponse> UpdateMerchant(string id, Merchant updatedMerchant)
        {
            throw new System.NotImplementedException();
        }
    }
}