using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Data.MongoDb.Helpers;
using MongoDB.Driver;


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
            var listingType = ListingTypeEnum.For(type);
            try
            {
                var userCurrentLocation = location.ToGeoJsonCoordinates();

                var locationQuery = new FilterDefinitionBuilder<ListingDto>().GeoWithinCenter(x => x.ListingAddress.Location, userCurrentLocation.Longitude, userCurrentLocation.Latitude, radius)
                   ;
                FilterDefinition<ListingDto> typeQuery;
                if (listingType != null)//all
                {
                    typeQuery = new FilterDefinitionBuilder<ListingDto>().Where(l => l.ListingType == listingType.ToString());
                }
                else
                {
                    typeQuery = new FilterDefinitionBuilder<ListingDto>().Empty;
                }

                query = await _db
                  .Find(locationQuery & typeQuery)
                  .Limit(10)
                  .ToListAsync();


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