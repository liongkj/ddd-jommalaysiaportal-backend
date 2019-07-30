using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        GetAllUserResponse GetAllUsers(int countperpage = 10, int page = 0);

        CreateUserResponse CreateUser(User user);
    }
}
