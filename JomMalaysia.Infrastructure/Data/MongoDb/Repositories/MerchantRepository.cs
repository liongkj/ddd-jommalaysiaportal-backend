using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Exceptions;
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

        public async Task FindBySsmIdAsync(string ssmId)
        {
            var query = await
                      _db.AsQueryable()
                                      .Where(M => M.CompanyRegistration.SsmId == ssmId)
                                      .Select(M => M)
                                      .FirstOrDefaultAsync();
            if (query != null)
                throw new DuplicatedException(ssmId, "Merchant is registered before.");
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

        public async Task<UpdateMerchantResponse> UpdateMerchantAsyncWithSession(string id, Merchant updatedMerchant, IClientSessionHandle session)
        {
            ReplaceOneResult result;
            var merchantDto = _mapper.Map<MerchantDto>(updatedMerchant);
            FilterDefinition<MerchantDto> filter = Builders<MerchantDto>.Filter.Eq(m => m.Id, id);
            try
            {
                result = await _db.ReplaceOneAsync(session, filter, merchantDto);
            }
            catch (Exception e)
            {
                return new UpdateMerchantResponse(new List<string> { "Update failed: Repository Error" }, false, e.Message);
            }
            return new UpdateMerchantResponse(id, result.IsModifiedCountAvailable, "update success");
        }
    }
}