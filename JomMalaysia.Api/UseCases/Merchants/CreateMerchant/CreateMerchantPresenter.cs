
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;

namespace JomMalaysia.Api.UseCases.Merchants.CreateMerchant
{
    public sealed class CreateMerchantPresenter : IOutputPort<CreateMerchantResponse>
    {
        public JsonContentResult ContentResult { get; }

        public CreateMerchantPresenter()
        {
            ContentResult = new JsonContentResult();
        }
 

        public void Handle(CreateMerchantResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
