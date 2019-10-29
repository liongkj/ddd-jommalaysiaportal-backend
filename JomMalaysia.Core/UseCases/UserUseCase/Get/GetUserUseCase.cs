using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        public GetUserUseCase(IUserRepository userRepository, ILoginInfoProvider loginInfo, IMapper mapper)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(GetUserRequest message, IOutputPort<GetUserResponse> outputPort)
        {
            GetUserResponse response;
            try
            {
                var getUserResponse = await _userRepository.GetUser(message.UserId);
                var vm = _mapper.Map<UserViewModel>(getUserResponse.User);
                response = new GetUserResponse(vm, getUserResponse.Success, getUserResponse.Message);

            }
            catch (Exception e)
            {
                throw e;
            }
            outputPort.Handle(response);

            return response.Success;
        }
    }
}
