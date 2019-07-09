using System.Collections.Generic;
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
using MongoDB.Driver.Linq;

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
            //mongodb driver api
            var result = _db.DeleteOne(filter: m => m.Id == merchantId);
            //todo TBC soft delete or hard delete
            return new DeleteMerchantResponse(merchantId, true);
        }

        public GetMerchantResponse FindById(string merchantId)
        {
            //linq to search with criteria
            var query =
                  _db.AsQueryable()
                  .Where(M => M.Id == merchantId)
                  .Select(M => M)
                  .FirstOrDefault();
            Merchant m = _mapper.Map<Merchant>(query);
            var response = m == null ? new GetMerchantResponse(new List<string> { "Merchant Not Found" }, false) : new GetMerchantResponse(m, true);
            return response;
        }

        public GetMerchantResponse FindByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public GetAllMerchantResponse GetAllMerchants()
        {
            var query =
                  _db.AsQueryable()
                  .ToList();
            List<Merchant> merchants = _mapper.Map<List<Merchant>>(query);
            var response = merchants.Count < 1 ?
                new GetAllMerchantResponse(new List<string> { "No Merchants" }, false) :
                new GetAllMerchantResponse(merchants, true);
            return response;
        }

        public UpdateMerchantResponse UpdateMerchant(string id, Merchant updatedMerchant)
        {
            ReplaceOneResult result = _db.ReplaceOne(merchant => merchant.Id == id, _mapper.Map<MerchantDto>(updatedMerchant));
            var response = result.ModifiedCount != 0 ? new UpdateMerchantResponse(id, true)
                : new UpdateMerchantResponse(new List<string>() { "update merchant failed" }, false);
            return response;
        }
    }
}