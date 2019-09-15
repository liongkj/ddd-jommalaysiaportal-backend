using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
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
    public CreateListingResponse CreateListing(Listing listing)
    {
        //notusing
        var Dto = _mapper.Map(
            listing,
            listing.GetType(),
            new ListingDto().GetType()
            );

        try
        {
            _db.InsertOne((ListingDto)Dto);
        }
        catch (Exception e)
        {
            var errors = new List<string> { e.ToString() };
            return new CreateListingResponse(errors);
        }
        return new CreateListingResponse(listing.ListingId, true);
    }

    public async Task<CreateListingResponse> CreateListingAsync(Listing listing, IClientSessionHandle session)
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
            return new CreateListingResponse(Dto.Id, true);
        }
        catch (Exception e)
        {
            return new CreateListingResponse(new List<string> { e.ToString() }, false, e.Message);
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
        if (item != null) return new GetListingResponse(Converted(item), true);
        else return new GetListingResponse(new List<string> { "Listing Not Found" }, false, "Listing Repo failed");
    }

    public GetListingResponse FindByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public async Task<GetAllListingResponse> GetAllListings()
    {
        GetAllListingResponse res;
        List<Listing> Mapped = new List<Listing>();
        try
        {
            var query =
                    await _db.AsQueryable()
                    //.OrderBy(c => c.ListingType)
                    .ToListAsync()
                    ;
            //var Listings = _mapper.Map<List<Listing>>(query);
            foreach (ListingDto list in query)
            {
                var temp = Converted(list);
                if (temp != null)
                {
                    Mapped.Add(temp);
                }
            }

            res = new GetAllListingResponse(Mapped, true);
        }
        catch (Exception e)
        {
            res = new GetAllListingResponse(new List<string> { "GetAllListingRepo" }, false, e.ToString() + e.Message);
        }

        return res;
    }



    public async Task<UpdateListingResponse> UpdateAsyncWithSession(Listing listing, IClientSessionHandle session = null)
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
            return new UpdateListingResponse(new List<string> { e.ToString() }, false, e.Message);
        }
        catch (Exception e)
        {
            return new UpdateListingResponse(new List<string> { e.ToString() }, false, e.Message);
        }
        return new UpdateListingResponse(listing.ListingId, result.IsAcknowledged, "update success");
    }




    #region private helper method
    private Listing Converted(ListingDto list)
    {
        if (list != null)
        {
            if (GetListingTypeHelper(list).Equals(typeof(EventListing)))
            {
                var i = _mapper.Map<EventListing>(list);

                return i;
            }

            if (GetListingTypeHelper(list).Equals(typeof(PrivateListing)))
            {
                var i = _mapper.Map<PrivateListing>(list);
                return i;
            }
        }
        return null;
    }

    private Type GetListingTypeHelper(ListingDto list)
    {
        if (list.ListingType == ListingTypeEnum.Event.ToString())
        {
            return typeof(EventListing);

        }
        if (list.ListingType == ListingTypeEnum.Private.ToString())
        {
            return typeof(PrivateListing);
        }
        throw new ArgumentException("Error taking listing info from database ");
        #endregion

    }
}