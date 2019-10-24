using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public class AdministrativeListing : Listing
    {
        private CoreListingRequest listing;
        private Address address;
        private Merchant merchant;

        public AdministrativeListing(CoreListingRequest listing, Address address, Merchant merchant)
        {
            this.listing = listing;
            this.address = address;
            this.merchant = merchant;
        }
    }
}