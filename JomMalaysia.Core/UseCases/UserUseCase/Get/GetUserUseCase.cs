using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;

        public GetUserUseCase(IUserRepository userRepository, ILoginInfoProvider loginInfo)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Handle(GetUserRequest message, IOutputPort<GetUserResponse> outputPort)
        {
            var response = await _userRepository.GetUser(message.UserId);


            outputPort.Handle(response);

            return response.Success;
        }
    }
}
