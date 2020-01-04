using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public interface IUpdatePublishListingUseCase
    {
        Task<bool> Handle(CoreListingRequest req, IOutputPort<CoreListingResponse> response, Listing listing);
    }
}