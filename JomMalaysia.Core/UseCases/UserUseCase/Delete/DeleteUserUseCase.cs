using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Delete
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;
        public DeleteUserUseCase(IUserRepository userRepository, ILoginInfoProvider loginInfo)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Handle(DeleteUserRequest message, IOutputPort<DeleteUserResponse> outputPort)
        {
            var appuser = _loginInfo.AuthenticatedUser();
            var GetUserResponse = await _userRepository.GetUser(message.Userid);
            if (GetUserResponse.Success)
            {
                var ToBeDelete = GetUserResponse.User;
                if (appuser.CanDelete(ToBeDelete))
                {
                    var DeleteUserResponse = await _userRepository.DeleteUser(message.Userid);
                    outputPort.Handle(DeleteUserResponse);
                    return DeleteUserResponse.Success;
                }
                else
                {
                    outputPort.Handle(new DeleteUserResponse(new List<string> { "You have no permission to delete this user" }));
                    return false;
                }
            }
            else
            {
                outputPort.Handle(new DeleteUserResponse(GetUserResponse.Errors, false, GetUserResponse.Message));
                return false;
            }


        }
    }
}