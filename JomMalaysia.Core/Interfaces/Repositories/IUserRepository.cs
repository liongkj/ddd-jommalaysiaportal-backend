using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Core.UseCases.UserUseCase.Get;
using JomMalaysia.Core.UseCases.UserUseCase.Update;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<GetAllUserResponse> GetAllUsers(int countperpage = 10, int page = 0);

        Task<GetUserResponse> GetUser(string userId);
        Task<CreateUserResponse> CreateUser(User user);
        Task<DeleteUserResponse> DeleteUser(string Userid);
        Task<UpdateUserResponse> UpdateUser(string userId, Tuple<List<string>, bool> updatedUserRole);
    }
}
