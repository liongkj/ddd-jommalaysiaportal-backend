using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;

        public GetAllUserUseCase(IUserRepository userRepository, ILoginInfoProvider loginInfo)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Handle(GetAllUserRequest message, IOutputPort<GetAllUserResponse> outputPort)
        {
            var AppUser = _loginInfo.AuthenticatedUser();
            var response = await _userRepository.GetAllUsers();

            if (!response.Success)
            {
                outputPort.Handle(new GetAllUserResponse(response.Errors));
                return false;
            }
            var ManageableUsers = AppUser.GetManageableUsers(response.Users);
            outputPort.Handle(new GetAllUserResponse(ManageableUsers, true));

            return response.Success;
        }
    }
}
