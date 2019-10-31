using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Framework.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;
        public CreateUserUseCase(
            IUserRepository userRepository,
            ILoginInfoProvider loginInfo
            )
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Handle(CreateUserRequest message, IOutputPort<CreateUserResponse> outputPort)
        {
            try
            {
                User NewUser = new User(message.Username, message.Name, message.Email);
                var updateRole = NewUser.AssignRole(message.Role);
                var createUserResponse = await _userRepository.CreateUser(NewUser);
                if (createUserResponse.Success)
                {
                    var response = await _userRepository.UpdateUser(createUserResponse.Status, updateRole);
                    if (!response.Success)
                    {
                        await _userRepository.DeleteUser(createUserResponse.Status); //rollback
                    }
                }
                outputPort.Handle(createUserResponse);
                return createUserResponse.Success;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
