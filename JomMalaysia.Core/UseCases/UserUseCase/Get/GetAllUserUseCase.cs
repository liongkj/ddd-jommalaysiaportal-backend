using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Exceptions;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Framework.Helper;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        public GetAllUserUseCase(IUserRepository userRepository, ILoginInfoProvider loginInfo, IMapper mapper)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(GetAllUserRequest message, IOutputPort<GetAllUserResponse> outputPort)
        {
            GetAllUserResponse response;
            try
            {
                var AppUser = _loginInfo.AuthenticatedUser();

                if (AppUser == null)
                {
                    throw new NotAuthorizedException();
                }
                var getAllUserResponse = await _userRepository.GetAllUsers();
                if (!getAllUserResponse.Success)
                {
                    outputPort.Handle(new GetAllUserResponse(getAllUserResponse.Errors));
                    return false;
                }
                var ManageableUsers = AppUser.GetManageableUsers(getAllUserResponse.Users);
                var mapped = _mapper.Map<PagingHelper<UserViewModel>>(ManageableUsers);


                response = new GetAllUserResponse(mapped, getAllUserResponse.Success, getAllUserResponse.Message);
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
