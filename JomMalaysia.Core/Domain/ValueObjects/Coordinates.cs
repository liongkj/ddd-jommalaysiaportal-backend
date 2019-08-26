using System.Collections.Generic;
using JomMalaysia.Core.Validation;
using MongoDB.Driver.GeoJsonObjectModel;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Coordinates : ValueObjectBase
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Coordinates(double Long, double Lat)
        {

            Longitude = Long;
            Latitude = Lat;


            var validator = new CoordinatesValidator();
            validator.Validate(this);
        }

        public GeoJson2DGeographicCoordinates ToGeoJsonCoordinates()
        {
            return new GeoJson2DGeographicCoordinates(Longitude, Latitude);
        }

        //Lat set between -90 ~ 90
        //Long set between -180 ~ 180


        private void ChangeCoordinates(double lon, double lat)
        {

            Latitude = lat;
            Longitude = lon;


        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
