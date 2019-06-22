using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Location : ValueObjectBase
    {
        public Address Address { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public Tuple<double, double> Coordinates { get; private set; }
        public ICollection<List<Tuple<double, double>>> Area { get; private set; }


        public void SetArea(List<Tuple<double, double>> Area)
        {

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
            yield return Latitude;
            yield return Longitude;
            yield return Coordinates;
            yield return Area;
        }

        private void SetCoordinates(double lon, double lat)
        {
            Latitude = lat;
            Longitude = lon;
            Coordinates = new Tuple<double, double>(lon, lat);

        }

    }

}
