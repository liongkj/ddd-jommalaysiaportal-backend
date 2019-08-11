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

    public DeleteListingResponse Delete(string id)
    {
        throw new System.NotImplementedException();
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