using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
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
            Address address = new Address("123", "s2a", "seremban", "seremban 2", "70300", "Malaysia");
            Name name = new Name("kj", "Liong");
            Email email = (Email)("khaijiet@hotmail.com");
            Phone phone = (Phone)("018-636789");
            var testOutputPort = new Mock<IOutputPort<CreateMerchantResponse>>();
            testOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<CreateMerchantResponse>()));


            //var merchant = new CreateMerchantRequest();
            // var response = useCase.Handle(merchant), testOutputPort.Object);

            //Assert.True(response);
        }
    }
}
