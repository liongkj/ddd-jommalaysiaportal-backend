
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

namespace JomMalaysia.Api.UseCases.Workflows
{
    public sealed class WorkflowPresenter : IOutputPort<UseCaseResponseMessage>
    {
        public JsonContentResult ContentResult { get; }

        public WorkflowPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 
        public void Handle(UseCaseResponseMessage response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
