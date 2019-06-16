
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Merchants.GetMerchant
{
    public sealed class GetMerchantPresenter : IOutputPort<GetMerchantResponse>
    {
        public JsonContentResult ContentResult { get; }

        public GetMerchantPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 

        public void Handle(GetMerchantResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
