using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.PlaceUseCase.Create;
using JomMalaysia.Infrastructure.Data.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    //http://mongodb.github.io/mongo-csharp-driver/2.8/reference/driver/crud/linq/
    public class PlaceRepository : IPlaceRepository
    {
        private readonly IMongoCollection<PlaceDto> _db;
        private readonly IMapper _mapper;
        public PlaceRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<PlaceDto>("Place");
        }

        public async Task<CreatePlaceResponse> Create(Place place)
        {
            var p = _mapper.Map<Place, PlaceDto>(place);
            await _db.InsertOneAsync(p);
            return new CreatePlaceResponse(p.Id, true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<PlaceDto> filter = Builders<PlaceDto>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _db
                                              .DeleteOneAsync(filter);
            var query = _db.AsQueryable()
            .Select(p => p.Id);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Place>> GetAllPlaces()
        {
            var places = await _db
                            .Find(_ => true)
                            .ToListAsync();
            return _mapper.Map<List<PlaceDto>, List<Place>>(places);
        }

        public async Task<string> GetNextId()
        {
            return await _db.CountDocumentsAsync(new BsonDocument()) + 1.ToString();
        }

        public async Task<Place> GetPlace(string id)
        {
            var filter = Builders<PlaceDto>.Filter.Eq(m => m.Id, id);
            var place = await _db
                    .Find(filter)
                    .FirstOrDefaultAsync();
            return _mapper.Map<PlaceDto, Place>(place);
        }

        public async Task<bool> Update(Place place)
        {
            var p = _mapper.Map<Place, PlaceDto>(place);
            ReplaceOneResult updateResult =
                 await _db
                         .ReplaceOneAsync(
                             filter: g => g.Id == p.Id,
                             replacement: p);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}