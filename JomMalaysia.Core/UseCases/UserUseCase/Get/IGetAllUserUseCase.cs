using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Request;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.UserUseCase
{
    public interface IGetAllUserUseCase : IUseCaseHandler<GetAllUserRequest, GetAllUserResponse>
    {
    }
}
