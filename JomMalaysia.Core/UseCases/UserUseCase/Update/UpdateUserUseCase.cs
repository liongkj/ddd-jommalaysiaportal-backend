using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //todo update user role
        public async Task<bool> Handle(UpdateUserRequest message, IOutputPort<UpdateUserResponse> outputPort)
        {

            var getUserResponse = await _userRepository.GetUser(message.UserId);
            if (!getUserResponse.Success)
            {
                outputPort.Handle(new UpdateUserResponse(new List<string> { getUserResponse.Error }, false, getUserResponse.Message));
                return false;
            }
            var OldUser = getUserResponse.User;
            var updatedUserRole = OldUser.UpdateRole(message.Role);
            if (updatedUserRole == null)
            {
                outputPort.Handle(new UpdateUserResponse(new List<string> { "Role not found / Role is not updated" }, false));

                return false;
            }
            var UpdateUserResponse = await _userRepository.UpdateUser(message.UserId, updatedUserRole);
            // OldUser.UpdateUser();
            outputPort.Handle(UpdateUserResponse);

            return UpdateUserResponse.Success;

        }
    }
}