using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Indexes;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Api.UseCases.Indexes
{
    public sealed class IndexPresenter : IOutputPort<UseCaseResponseMessage>
    {
        public JsonContentResult ContentResult { get; }

        public IndexPresenter()
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
