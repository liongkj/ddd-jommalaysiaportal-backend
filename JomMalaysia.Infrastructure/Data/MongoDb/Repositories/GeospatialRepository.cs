using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Data.MongoDb.Helpers;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class GeospatialRepository : IGeospatialRepository
    {
        private readonly IMongoCollection<ListingDto> _db;
        private readonly IMapper _mapper;
        public GeospatialRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<ListingDto>("Listing");
            // TODO how to add index
            _mapper = mapper;
        }

        public async Task<ListingResponse> GetListingsWithinRadius(Coordinates location, double radius, string type)
        {
            ListingResponse res;
            List<ListingDto> query;
            List<Listing> Mapped = new List<Listing>();
            try
            {
                var userCurrentLocation = location.ToGeoJsonCoordinates();
                var locationQuery = new FilterDefinitionBuilder<ListingDto>().GeoWithinCenter(x => x.ListingAddress.Location, userCurrentLocation.Longitude, userCurrentLocation.Latitude, radius);
                // (tag => tag.ListingAddress.Location, userCurrentLocation,
                // 50); //fetch results that are within a 50 metre radius of the point we're searching.
                query = await _db
                   .Find(locationQuery)
                   .Limit(10)
                   .ToListAsync(); //Limit the query to return only the top 10 results.

                foreach (ListingDto list in query)
                {
                    var temp = ListingDtoParser.Converted(_mapper, list);
                    if (temp != null)
                    {
                        Mapped.Add(temp);
                    }
                }
                res = new ListingResponse(Mapped, true, $"Returned {Mapped.Count} results");
            }
            catch (Exception ex)
            {
                res = new ListingResponse(new List<string> { "GetAllListingRepo" }, false, ex.ToString() + ex.Message);
            }
            return res;
        }
    }
}