using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Helpers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class ListingRepository : IListingRepository
    {

        private readonly IMongoCollection<ListingDto> _db;
        private readonly IMapper _mapper;
        public ListingRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<ListingDto>("Listing");

            _mapper = mapper;
        }


        public async Task<CoreListingResponse> CreateListingAsync(Listing listing, IClientSessionHandle session)
        {
            //var ListingDto = _mapper.Map<ListingDto>(listing);
            var dto = (ListingDto)_mapper.Map(
                listing,
                listing.GetType(),
                new ListingDto().GetType()
            );
            //TODO
            dto.CreatedAt = DateTime.UtcNow;
            try
            {
                await _db.InsertOneAsync(session, dto).ConfigureAwait(false);
                return new CoreListingResponse(dto.Id, true);
            }
            catch (Exception e)
            {
                return new CoreListingResponse(new List<string> { e.ToString() }, false, e.Message);
            }

        }

        public async Task<DeleteListingResponse> DeleteAsyncWithSession(string id, IClientSessionHandle session)
        {
            DeleteResult result;
            try
            {
                result = await _db.DeleteOneAsync(session, l => l.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return new DeleteListingResponse(id, false, e.Message);
            }
            return new DeleteListingResponse(id, result.IsAcknowledged, "Repo delete operation");

        }

        public async Task<GetListingResponse> FindById(string id)
        {
            ListingDto item;
            Listing mapped;
            try
            {
                var query =
                    await _db.AsQueryable()
                        .Where(m => m.Id == id)
                        .Select(m => m)
                        .FirstOrDefaultAsync();
                
                item = _mapper.Map<ListingDto>(query);
                mapped = ListingDtoParser.Converted(_mapper, item);
            }
            catch (Exception e)
            {
                return new GetListingResponse(new List<string> { "Get Listing Error" }, false, e.Message);
            }
            return item != null ? new GetListingResponse(mapped, true)
                : new GetListingResponse(new List<string> { "Listing Not Found" }, false, "Listing Repo failed");
        }

        public  Task<GetListingResponse> FindByName(string name)
        {
            throw new NotImplementedException();
            //TODO If need this function
        }

        public async Task<GetAllListingResponse> GetAllListings(CategoryPath cp, string type, bool groupBySub, string status, string city, bool isFeatured)
        {
            GetAllListingResponse res;
            List<ListingDto> query;
            List<Listing> mapped = new List<Listing>();
            var parsed = Enum.TryParse(type, true, out CategoryType ct);
            var publishStatus = ListingStatusEnum.For(status);

            var builder = Builders<ListingDto>.Filter;
            var filter = builder.Empty;
            try
            {
                //category filter
                if (cp != null)
                {
                    FilterDefinition<ListingDto> categoryFilter;
                    if (groupBySub)
                    {
                        categoryFilter = builder.Eq(ld => ld.Category.ToString(), cp.ToString());
                    }
                    else
                    {
                        categoryFilter = builder.Where(ld => ld.Category.ToString().StartsWith(cp.ToString()));
                    }
                    filter &= categoryFilter;
                }
                //publish status filter
                if (publishStatus != null)
                {
                    var statusFilter = builder.Eq(ld => ld.PublishStatus.Status, publishStatus.ToString());
                    filter &= statusFilter;
                }
                //category type filter
                if (parsed)
                {
                    var typeFilter = builder.Where(ld => ld.CategoryType.Equals(ct.ToString()) || ld.CategoryType.Equals(ct.ToString().ToLower()));
                    filter &= typeFilter;
                }
                if (!String.IsNullOrEmpty(city))
                {
                    var cityFilter = builder.Where(ld => ld.ListingAddress.City.ToLower() == city.ToLower());
                    filter &= cityFilter;
                }

                if (isFeatured)
                {
                    var featuredFilter = builder.Where(ld => ld.IsFeatured);
                    filter &= featuredFilter;
                }

                query = await _db.Find(filter)
                    .ToListAsync();


                foreach (ListingDto list in query)
                {
                    var temp = ListingDtoParser.Converted(_mapper, list);
                    if (temp != null)
                    {
                        mapped.Add(temp);
                    }
                }

                res = new GetAllListingResponse(mapped, true, $"Returned {mapped.Count} results");
            }
            catch (Exception e)
            {
                res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, e + e.Message);
            }

            return res;
        }

        public async Task<GetAllListingResponse> GetAllListings(CategoryPath cp, bool isSameSubcategory = true)
        {
            GetAllListingResponse res;
            List<ListingDto> query;
            List<Listing> mapped = new List<Listing>();

            var builder = Builders<ListingDto>.Filter;
            var filter = builder.Empty;
            try
            {
                if (cp != null)
                {
                    filter &= builder.Eq(ld => ld.Category.Category, cp.Category.ToLower());

                    if (isSameSubcategory)
                    {
                        filter &= builder.Eq(ld => ld.Category.Subcategory, cp.Subcategory.ToLower());
                    }




                }
                query = await _db.Find(filter)
                        .ToListAsync();
                foreach (ListingDto list in query)
                {
                    var temp = ListingDtoParser.Converted(_mapper, list);
                    if (temp != null)
                    {
                        mapped.Add(temp);
                    }
                }
                res = new GetAllListingResponse(mapped, true, $"Returned {mapped.Count} results");
            }
            catch (Exception e)
            {
                res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, e + e.Message);
            }

            return res;
        }



        public async Task<CoreListingResponse> UpdateAsyncWithSession(Listing listing, IClientSessionHandle session = null)
        {
            ReplaceOneResult result;

            FilterDefinition<ListingDto> filter = Builders<ListingDto>.Filter.Eq(m => m.Id, listing.ListingId);
            try
            {
                var listingDto = _mapper.Map<ListingDto>(listing);
                listingDto.ModifiedAt = DateTime.Now;
                if (session != null)
                    result = await _db.ReplaceOneAsync(session, filter, listingDto);
                else result = await _db.ReplaceOneAsync(filter, listingDto);
            }
            catch (AutoMapperMappingException e)
            {
                return new CoreListingResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            catch (Exception e)
            {
                return new CoreListingResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            return new CoreListingResponse(listing.ListingId, result.IsAcknowledged, "update success");
        }

        public async Task<CoreListingResponse> UpdateCategoryAsyncWithSession(Dictionary<string, CategoryPath> toBeUpdateListings, IClientSessionHandle session)
        {
            BulkWriteResult result;
            var bulkOps = new List<WriteModel<ListingDto>>();
            try
            {
                foreach (var list in toBeUpdateListings)
                {
                    var filter = Builders<ListingDto>.Filter.Where(l => l.Id == list.Key);

                    var update = Builders<ListingDto>.Update.Set(l => l.Category, list.Value);


                    var updateOne = new UpdateOneModel<ListingDto>(filter, update);
                    //only valid for update subcategories
                    bulkOps.Add(updateOne);


                }
                result = await _db.BulkWriteAsync(session, bulkOps);
            }

            catch (Exception e)
            {
                return new CoreListingResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            return new CoreListingResponse(new List<string> { "Listing updated" }, result.IsAcknowledged, $" {result.ModifiedCount} listings updated.");
        }



    }
}