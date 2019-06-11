using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Core.Services.UseCaseResponses;
using JomMalaysia.Core.UseCases;
using Moq;
using Xunit;

namespace JomMalaysia.Test.UseCases
{
    public class CreateMerchantUseCaseUnitTest
    {
        [Fact]
        public void Can_Create_Merchant()
        {
            //store the user data somehow
            var testMerchantRepository = new Mock<IMerchantRepository>();
            testMerchantRepository
                .Setup(repo => repo.CreateMerchant(It.IsAny<Merchant>()))
                .Returns(new CreateMerchantResponse("", true));

            //use case
            var useCase = new CreateMerchantUseCase(testMerchantRepository.Object);
            Address address = new Address
            {
                Add1 = "123"
            };
            var testOutputPort = new Mock<IOutputPort<CreateMerchantResponse>>();
            testOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<CreateMerchantResponse>()));

            var response = useCase.Handle(new CreateMerchantRequest("KFC", "002623588-K", "kj", "liong", address, "0123456789", "031324123"),testOutputPort.Object);

            Assert.True(response);
        }
    }
}
