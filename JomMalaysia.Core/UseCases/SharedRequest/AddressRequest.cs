using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.SharedRequest
{
    public class AddressRequest
    {
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public StateEnum State { get; set; }
        public string PostalCode { get; set; }
        public CountryEnum Country { get; set; }
        public CoordinateRequest Coordinates { get; set; }
    }
}