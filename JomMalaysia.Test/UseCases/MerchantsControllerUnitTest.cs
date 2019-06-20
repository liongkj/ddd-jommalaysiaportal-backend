
using System.Net;

using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;
using JomMalaysia.Core.UseCases;
using JomMalaysia.Core.Services.Merchants.UseCaseRequests;

using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;

namespace JomMalaysia.Test.UseCases
{

    public class MerchantsControllerUnitTest
    {
        [Fact]
        public void AddValidMerchant_Return()
        {
            // arrange


            // fakes


            // act


            // assert

        }
        [Fact]
        public void AddInValidMerchant_ReturnCreatedResponse()

        {
            //// arrange
            //var controller = new MerchantController(null, null);
            //controller.ModelState.AddModelError("FirstName", "Required");

            //// act
            //var result = controller.Post(null);

            //// assert
            //var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            //Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}
