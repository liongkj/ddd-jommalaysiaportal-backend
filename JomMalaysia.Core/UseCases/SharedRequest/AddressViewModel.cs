using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.UseCases.SharedRequest
{
    public class AddressViewModel
    {
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}