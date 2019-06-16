
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Merchants.DeleteMerchant
{
    public sealed class DeleteMerchantPresenter : IOutputPort<DeleteMerchantResponse>
    {
        public JsonContentResult ContentResult { get; }

        public DeleteMerchantPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 

        public void Handle(DeleteMerchantResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
