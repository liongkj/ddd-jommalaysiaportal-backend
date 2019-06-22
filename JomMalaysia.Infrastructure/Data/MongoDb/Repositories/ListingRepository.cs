using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly IMongoCollection<ListingDto> _db;
        public readonly IMapper _mapper;
        MongoClient client;

        public ListingRepository(IApplicationDbContext settings, IMapper mapper)
        {
            client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _db = database.GetCollection<ListingDto>("Listing");
            _mapper = mapper;
        }

        public CreateListingResponse CreateListing(Listing merchant)
        {
            ListingDto NewListing = _mapper.Map<Listing, ListingDto>(merchant);
            try
            {
                var session = client.StartSession();
                //TODO
                _db.InsertOne(NewListing);
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


        public async Task<GetAllListingResponse> GetAllListings()
        {
            var result =
                await _db.Find(md => true).ToListAsync();
            var merchants = _mapper.Map<List<ListingDto>, List<Listing>>(result);

            return new GetAllListingResponse(merchants, true);

        }


        public DeleteListingResponse Delete(string id)
        {
            try
            {
                _db.DeleteOne(m => m.Id == id);
                //TODO
                //Soft Delete
            }
            catch (Exception ex)
            {
                return new DeleteListingResponse((IEnumerable<string>)ex, false, "mongodb: Listing delete failed");
            }
            return new DeleteListingResponse(id, true, "Listing deleted successfully");
        }

        public GetListingResponse FindById(string id)
        {
            //ListingDto listing = _db.Find(m => m.Id == id).FirstOrDefault();
            //var found = _mapper.Map<ListingDto, Listing>(listing);
            //return new GetListingResponse( "Found by id");
            throw new NotImplementedException();
        }

        public GetListingResponse FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public UpdateListingResponse Update(string id, Listing newListing)
        {
            ListingDto m = _mapper.Map<Listing, ListingDto>(newListing);
            _db.ReplaceOne(md => md.Id == id, m);
            return new UpdateListingResponse(m.Id, true, "Listing " + m.Id + " updated");
        }


    }
}
