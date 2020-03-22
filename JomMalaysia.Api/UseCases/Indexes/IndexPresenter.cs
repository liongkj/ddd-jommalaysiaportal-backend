using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Indexes;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Api.UseCases.Indexes
{
    public sealed class IndexPresenter : IOutputPort<IndexResponse>
    {
        public JsonContentResult ContentResult { get; }

        public IndexPresenter()
        {
            ContentResult = new JsonContentResult();
        }
        
        public void Handle(IndexResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
