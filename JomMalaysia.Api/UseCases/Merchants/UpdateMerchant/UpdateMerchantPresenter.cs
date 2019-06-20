
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Merchants.GetMerchant
{
    public sealed class UpdateMerchantPresenter : IOutputPort<UpdateMerchantResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UpdateMerchantPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 

        public void Handle(UpdateMerchantResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
