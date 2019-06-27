
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetListingRequest:IUseCaseRequest<GetListingResponse>
    {
        public string Id { get; }

        public GetListingRequest(string ListingId)
        {
            Id = ListingId;
        }
    }
}
