using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly IMongoCollection<MerchantDto> _merchants;
        public readonly IMapper _mapper;

        public MerchantRepository(IApplicationDbContext settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _merchants = database.GetCollection<MerchantDto>("Merchant");
            _mapper = mapper;
        }

        public CreateMerchantResponse CreateMerchant(Merchant merchant)
        {
            MerchantDto NewMerchant = _mapper.Map<Merchant, MerchantDto>(merchant);
            try
            {
                _merchants.InsertOne(NewMerchant);
                return new CreateMerchantResponse(NewMerchant.Id, true);
            }
            catch (MongoWriteException e)
            {
                return new CreateMerchantResponse(e.ToString());
            }
            catch (MongoWriteConcernException e)
            {
                return new CreateMerchantResponse(e.ToString());
            }
            catch (MongoException e)
            {
                return new CreateMerchantResponse(e.ToString());
            }
        }


        public async Task<GetAllMerchantResponse> GetAllMerchants()
        {
            var result =
                await _merchants
                .Find("{ }")
                .ToListAsync();
            List<Merchant> merchants = new List<Merchant>();
            foreach (var merchant in result)
            {
                merchants.Add(_mapper.Map<MerchantDto, Merchant>(merchant));
            }
            return new GetAllMerchantResponse(merchants, true);

        }
        //public async Task<CreateUserResponse> Create(User user, string password)
        //{
        //    var appUser = _mapper.Map<AppUser>(user);
        //    var identityResult = await _userManager.CreateAsync(appUser, password);
        //    return new CreateUserResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
        //}

        public DeleteMerchantResponse Delete(string id)
        {
            try
            {
                _merchants.DeleteOne(m => m.Id == id);
            }
            catch (Exception ex)
            {
                return new DeleteMerchantResponse(ex.ToString());
            }
            return new DeleteMerchantResponse(id, true);
        }

        public GetMerchantResponse FindById(string id)
        {
            MerchantDto merchant = _merchants.Find(m => m.Id == id).FirstOrDefault();
            var found = _mapper.Map<MerchantDto, Merchant>(merchant);
            return new GetMerchantResponse(found, true);
        }

        public GetMerchantResponse FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public UpdateMerchantResponse Update(string id, Merchant merchant)
        {
            throw new NotImplementedException();
        }


    }
}
