using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<GetAllUserResponse> GetAllUsers(int countperpage = 10, int page = 0);

        Task<CreateUserResponse> CreateUser(User user);
        Task<DeleteUserResponse> DeleteUser(string Userid);
    }
}
