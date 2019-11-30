using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Data.MongoDb.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings
{
    public class ListingDto : IEntityDateTime, IListingDto
    {
        //add all possible properties here, etc. event listing has start date and end date
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MerchantSummaryDto Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string CategoryType { get; set; }
        public AddressDto ListingAddress { get; set; }
        public List<StoreTimesDto> OperatingHours { get; set; }
        public List<ServiceDto> ProvidedServices { get; set; }
        public List<GovDepartmentDto> Departments { get; set; }

        public string Category { get; set; }
        
        public ICollection<string> Tags { get; set; }
        public ListingImages ListingImages { get; set; } = new ListingImages
        {
            ListingLogo = new Image(),
            CoverPhoto = new Image(),

        };

        public PublishStatusDto PublishStatus { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime EventStartDateTime { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime EventEndDateTime { get; set; }
        public BsonDateTime CreatedAt { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime ModifiedAt { get; set; }
    }

    public class GovDepartmentDto
    {
        public string Name { get; set; }
        public string ServiceDescription { get; set; }
    }
    
    public class ServiceDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}