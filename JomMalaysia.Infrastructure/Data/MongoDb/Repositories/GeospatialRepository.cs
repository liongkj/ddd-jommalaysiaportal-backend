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
using MongoDB.Driver;

using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Infrastructure.Helpers;

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

        public async Task<GetAllListingResponse> ListingSearch(String k, String locale)
        {
            GetAllListingResponse res;
            List<ListingDto> query;
            List<Listing> Mapped = new List<Listing>();
            try
            {
                var filter = Builders<ListingDto>.Filter.Text(k, locale);

                query = await _db
                                 .Find(filter)
                                 .ToListAsync();

                foreach (ListingDto list in query)
                {
                    var temp = ListingDtoParser.Converted(_mapper, list);
                    if (temp != null)
                    {
                        Mapped.Add(temp);
                    }
                }
                res = new GetAllListingResponse(Mapped, true, $"Returned {Mapped.Count} results");
            }
            catch (Exception ex)
            {
                res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, ex + ex.Message);
            }
            return res;
        }

        public async Task<GetAllListingResponse> GetListingsWithinRadius(Coordinates location, double radius, string type)
        {
            GetAllListingResponse res;
            List<ListingDto> query;
            List<Listing> Mapped = new List<Listing>();
            var parsed = Enum.TryParse(type, out CategoryType typeFilter);
            try
            {
                var userCurrentLocation = location.ToGeoJsonCoordinates();

                var locationQuery = new FilterDefinitionBuilder<ListingDto>().GeoWithinCenter(x => x.ListingAddress.Location, userCurrentLocation.Longitude, userCurrentLocation.Latitude, radius)
                   ;
                FilterDefinition<ListingDto> typeQuery;
                typeQuery = parsed ? new FilterDefinitionBuilder<ListingDto>().Where(l => l.CategoryType == typeFilter.ToString()) : new FilterDefinitionBuilder<ListingDto>().Empty;

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
                res = new GetAllListingResponse(Mapped, true, $"Returned {Mapped.Count} results");
            }
            catch (Exception ex)
            {
                res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, ex + ex.Message);
            }
            return res;
        }
    }
}