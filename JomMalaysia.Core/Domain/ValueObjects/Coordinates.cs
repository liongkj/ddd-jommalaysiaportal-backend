using System;
using System.Collections.Generic;
using JomMalaysia.Core.Exceptions;
using JomMalaysia.Core.Validation;
using MongoDB.Driver.GeoJsonObjectModel;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Coordinates : ValueObjectBase
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Coordinates(string Long, string Lat)
        {
            if (string.IsNullOrEmpty(Long))
            {
                throw new ArgumentException("message", nameof(Long));
            }

            if (string.IsNullOrEmpty(Lat))
            {
                throw new ArgumentException("message", nameof(Lat));
            }

            Double x, y;
            if (Double.TryParse(Long, out x))
            {
                Longitude = x;
            }
            else
            {
                throw new ValidationException();
            }
            if (Double.TryParse(Lat, out y))
            {
                Latitude = y;
            }
            else
            {
                throw new Exception("Latitude is invalid");
            }

        }
        public Coordinates(Double Long, Double Lat)
        {
            Longitude = Long;
            Latitude = Lat;
        }
        public GeoJson2DGeographicCoordinates ToGeoJsonCoordinates()
        {
            return new GeoJson2DGeographicCoordinates(Longitude, Latitude);
        }

        public static Coordinates For(GeoJson2DGeographicCoordinates coordinates)
        {
            return new Coordinates(coordinates.Longitude, coordinates.Latitude);
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
