
using System.Net;
using JomMalaysia.Api.UseCases;
using JomMalaysia.Api.UseCases.CreateMerchant;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;
using JomMalaysia.Core.UseCases;
using Xunit;

using Moq;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Test.UseCases
{

    public class MerchantControllerUnitTest
    {
        [Fact]
        public void AddValidMerchant_Return()
        {
            // arrange
            var mockMerchantRepository = new Mock<IMerchantRepository>();
            mockMerchantRepository
                .Setup(repo => repo.CreateMerchant(It.IsAny<Merchant>()))
                .Returns(new CreateMerchantResponse("", true));

            // fakes
            var outputPort = new CreateMerchantPresenter();
            var useCase = new CreateMerchantUseCase(mockMerchantRepository.Object);

            var controller = new MerchantController(useCase, outputPort);

            // act
            var result = controller.Post(new CreateMerchantRequest());

            // assert
            var statusCode = ((ContentResult)result).StatusCode;
            Assert.True(statusCode.HasValue && statusCode.Value == (int)HttpStatusCode.OK);
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
