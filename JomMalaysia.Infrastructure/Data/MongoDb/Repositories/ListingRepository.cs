using System;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly IMongoCollection<ListingDto> _db;

        public readonly IMapper _mapper;

        private readonly IMongoDbContext _context;
        public ListingRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<ListingDto>("Listing");

            _mapper = mapper;
        }

        public async Task<CreateListingResponse> CreateListing(IMongoClient client, IClientSessionHandle session, Listing listing)
        {

            await _context.StartSession();
            var listingdb = client.GetDatabase("jomn9").GetCollection<ListingDto>("Listing");
            var categorydb = client.GetDatabase("jomn9").GetCollection<CategoryDto>("Category");
            ListingDto NewListing = _mapper.Map<Listing, ListingDto>(listing);

            session.StartTransaction(new TransactionOptions(readConcern: ReadConcern.Snapshot, writeConcern: WriteConcern.WMajority));
            try
            {
                var filter = Builders<SubcategoryDto>.Filter.Eq(c => c.Id, NewListing.SubcategoryDto.Id);
                var update = Builders<SubcategoryDto>.Update.Push(cd => cd.ListingIds, NewListing.Id);
                listingdb.InsertOne(session, NewListing);

                //TODO

                return new CreateListingResponse(NewListing.Id, true);
            }
            catch (MongoWriteException e)
            {
                return new CreateListingResponse(e.ToString());
            }
            catch (MongoWriteConcernException e)
            {
                return new CreateListingResponse(e.ToString());
            }
            catch (MongoException e)
            {
                return new CreateListingResponse(e.ToString());
            }
        }

        public Task<CreateListingResponse> CreateListing(Listing listing)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllListingResponse> GetAllListings()
        {
            throw new NotImplementedException();
        }

        public DeleteListingResponse Delete(string id)
        {
            throw new NotImplementedException();
        }

        public GetListingResponse FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public GetListingResponse FindById(string id)
        {
            throw new NotImplementedException();
        }

        public UpdateListingResponse Update(string id, Listing listing)
        {
            throw new NotImplementedException();
        }
    }
}
