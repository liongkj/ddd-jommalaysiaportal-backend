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

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{

    //https://github.com/dj-nitehawk/MongoDB.Entities/wiki/1.-Getting-Started
    public class MerchantRepository : IMerchantRepository
    {
        private readonly IMongoCollection<MerchantDto> _db;
        public readonly IMapper _mapper;

        public MerchantRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<MerchantDto>("Merchant");

            _mapper = mapper;
        }

        public CreateMerchantResponse CreateMerchant(Merchant merchant)
        {
            MerchantDto NewMerchant = _mapper.Map<Merchant, MerchantDto>(merchant);
            try
            {
                _db.InsertOne(NewMerchant);
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
                await _db.Find(md => true).ToListAsync();
            var merchants = _mapper.Map<List<MerchantDto>, List<Merchant>>(result);

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
                _db.DeleteOne(m => m.Id == id);
                //TODO
                //Soft Delete
            }
            catch (Exception ex)
            {
                return new DeleteMerchantResponse((IEnumerable<string>)ex, false, "mongodb: Merchant delete failed");
            }
            return new DeleteMerchantResponse(id, true, "Merchant deleted successfully");
        }

        public GetMerchantResponse FindById(string id)
        {
            MerchantDto merchant = _db.Find(m => m.Id == id).FirstOrDefault();
            var found = _mapper.Map<MerchantDto, Merchant>(merchant);
            return new GetMerchantResponse(found, true, merchant.CompanyName + "Found by id");
        }

        public GetMerchantResponse FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public UpdateMerchantResponse Update(string id, Merchant newMerchant)
        {
            MerchantDto m = _mapper.Map<Merchant, MerchantDto>(newMerchant);
            _db.ReplaceOne(md => md.Id == id, m);
            return new UpdateMerchantResponse(m.Id, true, "Merchant " + m.Id + " updated");
        }


    }
}
