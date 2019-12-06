namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class OfficialContactRequest
    {
        public string MobileNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}