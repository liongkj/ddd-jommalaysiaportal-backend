//ToDO
using System;
using System.Collections.Generic;
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
        var ListingDto = _mapper.Map<Listing, ListingDto>(listing);

        try
        {
            _db.InsertOne(ListingDto);
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
        var ListingDto = _mapper.Map<ListingDto>(listing);
        try
        {
            await _db.InsertOneAsync(session, ListingDto).ConfigureAwait(false);
            return new CreateListingResponse(ListingDto.Id, true);
        }
        catch (Exception e)
        {
            return new CreateListingResponse(new List<string> { e.ToString() }, false, e.Message);
        }
        
    }

    public DeleteListingResponse Delete(string id)
    {
        throw new NotImplementedException();
    }

    public GetListingResponse FindById(string id)
    {
        throw new System.NotImplementedException();
    }

    public GetListingResponse FindByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public Task<GetAllListingResponse> GetAllListings()
    {
        throw new System.NotImplementedException();
    }

    public UpdateListingResponse Update(string id, Listing listing)
    {
        throw new System.NotImplementedException();
    }
}