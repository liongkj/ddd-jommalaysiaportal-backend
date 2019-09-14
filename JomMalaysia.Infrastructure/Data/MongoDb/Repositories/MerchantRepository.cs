using System;
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
        public async Task<CreateMerchantResponse> CreateMerchantAsync(Merchant merchant)
        {
            var merchantDto = _mapper.Map<Merchant, MerchantDto>(merchant);
            try
            {
                await _db.InsertOneAsync(merchantDto);
            }
            catch (Exception e)
            {
                return new CreateMerchantResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            return new CreateMerchantResponse(merchant.MerchantId, true);
        }

        public async Task<DeleteMerchantResponse> DeleteMerchantAsync(string merchantId)
        {
            //mongodb driver api
            try
            {
                await _db.DeleteOneAsync(filter: m => m.Id == merchantId).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return new DeleteMerchantResponse(new List<string> { e.ToString() }, false, "delete merchant repo error");
            }
            //todo TBC soft delete or hard delete
            return new DeleteMerchantResponse(merchantId, true, "Merchant deleted successfully");
        }

        public async Task<GetMerchantResponse> FindByIdAsync(string merchantId)
        {
            Merchant m;
            //linq to search with criteria
            try
            {
                var query = await
                      _db.AsQueryable()
                      .Where(M => M.Id == merchantId)
                      .Select(M => M)
                      .FirstOrDefaultAsync();
                m = _mapper.Map<Merchant>(query);
            }
            catch (Exception e)
            {
                return new GetMerchantResponse(new List<string> { e.ToString() }, false, "fetch merchant error");
            }
            if (m != null)
                return new GetMerchantResponse(m, true);
            return new GetMerchantResponse(new List<string> { merchantId + " Not Found" }, false, "Merchant not found");

        }

        public GetMerchantResponse FindByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetAllMerchantResponse> GetAllMerchantAsync()
        {
            List<Merchant> merchants;
            try
            {
                var query = await
                      _db.AsQueryable()
                      .ToListAsync().ConfigureAwait(false);
                merchants = _mapper.Map<List<Merchant>>(query);

                //TODO fix mapping profile
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            var response = merchants.Count < 1 ?
                new GetAllMerchantResponse(new List<string> { "No Merchants" }, false) :
                new GetAllMerchantResponse(merchants, true, $"{merchants.Count} result found");
            return response;
        }

        public UpdateMerchantResponse UpdateMerchant(string id, Merchant updatedMerchant)
        {
            ReplaceOneResult result = _db.ReplaceOne(merchant => merchant.Id == id, _mapper.Map<MerchantDto>(updatedMerchant));
            var response = result.ModifiedCount != 0 ? new UpdateMerchantResponse(id, true)
                : new UpdateMerchantResponse(new List<string>() { "update merchant failed" }, false);
            return response;
        }

        public async Task<UpdateMerchantResponse> UpdateMerchant(string id, Merchant updatedMerchant, IClientSessionHandle session)
        {
            var merchantDto = _mapper.Map<MerchantDto>(updatedMerchant);
            FilterDefinition<MerchantDto> filter = Builders<MerchantDto>.Filter.Eq(m => m.Id, id);
            try
            {
                var result = await _db.ReplaceOneAsync(session, filter, merchantDto);
            }
            catch (Exception e)
            {
                return new UpdateMerchantResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            return new UpdateMerchantResponse(id, true, "update success");
        }
    }
}