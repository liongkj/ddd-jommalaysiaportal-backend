using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver.GeoJsonObjectModel;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class AddressDto
    {
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

    }
}