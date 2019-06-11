﻿using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Merchants.GetAllMerchant
{
    public sealed class GetAllMerchantPresenter : IOutputPort<GetAllMerchantResponse>
    {
        public JsonContentResult ContentResult { get; }

        public GetAllMerchantPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetAllMerchantResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
