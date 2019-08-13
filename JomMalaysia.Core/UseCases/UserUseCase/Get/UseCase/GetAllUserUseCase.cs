using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Request;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get.UseCase
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

        public bool Handle(GetAllUserRequest message, IOutputPort<GetAllUserResponse> outputPort)
        {
            //validate request
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var response = _userRepository.GetAllUsers();

            if (!response.Success)
            {
                outputPort.Handle(new GetAllUserResponse(response.Errors));
            }
            outputPort.Handle(new GetAllUserResponse(response.Users, true));

            return response.Success;
        }
    }
}
