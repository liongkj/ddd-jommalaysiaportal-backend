
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Listings.CreateListing
{
    public sealed class CreateListingPresenter : IOutputPort<CreateListingResponse>
    {
        public JsonContentResult ContentResult { get; }

        public CreateListingPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 

        public void Handle(CreateListingResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
