using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Data.MongoDb.Helpers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        var Dto = (ListingDto)_mapper.Map(
           listing,
           listing.GetType(),
           new ListingDto().GetType()
           );
        Dto.CreatedAt = DateTime.UtcNow;
        try
        {
            await _db.InsertOneAsync(session, Dto).ConfigureAwait(false);
            return new CoreListingResponse(Dto.Id, true);
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
        try
        {
            var query =
                     await _db.AsQueryable()
                  .Where(M => M.Id == id)
                  .Select(M => M)
                  .FirstOrDefaultAsync();
            ;

            item = _mapper.Map<ListingDto>(query);

        }
        catch (Exception e)
        {
            return new GetListingResponse(new List<string> { "Get Listing Error" }, false, e.Message);
        }
        if (item != null) return new GetListingResponse(ListingDtoParser.Converted(_mapper, item), true);
        else return new GetListingResponse(new List<string> { "Listing Not Found" }, false, "Listing Repo failed");
    }

    public async Task<GetListingResponse> FindByName(string name)
    {
        throw new System.NotImplementedException();
        //TODO If need this function
    }

    public async Task<GetAllListingResponse> GetAllListings(CategoryPath cp, string type, bool groupBySub)
    {
        GetAllListingResponse res;
        List<ListingDto> query;
        List<Listing> Mapped = new List<Listing>();
        var listingType = ListingTypeEnum.For(type);

        var builder = Builders<ListingDto>.Filter;
        var filter = builder.Empty;
        try
        {
            if (cp != null)
            {
                FilterDefinition<ListingDto> categoryFilter;
                if (groupBySub)
                {
                    categoryFilter = builder.Eq(ld => ld.Category, cp.ToString());

                }
                else
                {
                    categoryFilter = builder.Where(ld => ld.Category.StartsWith(cp.ToString()));
                }
                filter = filter & categoryFilter;
            }
            if (listingType != null)
            {
                var typeFilter = builder.Eq(ld => ld.ListingType, listingType.ToString());
                filter = filter & typeFilter;
            }

            query = await _db.Find(filter)
            .ToListAsync();


            // if (cp == null)
            // {//return all
            //     if (listingType == null)
            //         query =
            //                 await _db.AsQueryable()
            //               .ToListAsync();
            //     else
            //     {//return by listing type
            //         query = await _db.AsQueryable()
            //         .Where(l => l.ListingType.Equals(listingType.ToString()))
            //         .ToListAsync();
            //     }
            // }
            // else
            // {//return by category, by subcategory
            //     if (groupBySub)
            //     {
            //         query = await _db.AsQueryable()
            //             .Where(l => l.Category.Equals(cp.ToString()))
            //             .ToListAsync();
            //     }
            //     else
            //     {
            //         query = await _db.AsQueryable()
            //            .Where(l => l.Category.StartsWith(cp.ToString()))
            //            .ToListAsync();
            //     }
            // }

            //var Listings = _mapper.Map<List<Listing>>(query);
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
        catch (Exception e)
        {
            res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, e.ToString() + e.Message);
        }

        return res;
    }



    public async Task<CoreListingResponse> UpdateAsyncWithSession(Listing listing, IClientSessionHandle session = null)
    {
        ReplaceOneResult result;

        FilterDefinition<ListingDto> filter = Builders<ListingDto>.Filter.Eq(m => m.Id, listing.ListingId);
        try
        {
            var ListingDto = _mapper.Map<ListingDto>(listing);
            if (session != null)
                result = await _db.ReplaceOneAsync(session, filter, ListingDto);
            else result = await _db.ReplaceOneAsync(filter, ListingDto);
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

    public async Task<CoreListingResponse> UpdateCategoryAsyncWithSession(Dictionary<string, string> toBeUpdateListings, IClientSessionHandle session)
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