using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver.GeoJsonObjectModel;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Location : ValueObjectBase
    {
        public List<Coordinates> Coordinates { get; private set; }


        public Location(List<Coordinates> Coordinates)
        {
            this.Coordinates = Coordinates;
        }

        public GeoJsonGeometry<GeoJson2DGeographicCoordinates> ToGeoJsonPoint()
        {
            if (Coordinates.Count == 1)
                return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(Coordinates[0].ToGeoJsonCoordinates());
            return null;
            // else return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(Coordinates[0].Longitude, Coordinates[0].Latitude));

        }

        public void SetArea(List<Tuple<double, double>> Area)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {

            yield return Coordinates;

        }


    }

}
