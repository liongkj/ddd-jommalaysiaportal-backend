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
            // var getUserResponse = await _userRepository.GetUser(message.UserId);
            // if (!getUserResponse.Success)
            // {
            //     outputPort.Handle(new UpdateUserResponse(new List<string> { getUserResponse.Error }, false, getUserResponse.Message));
            //     return false;
            // }
            // var OldUser = getUserResponse.User;
            // OldUser.UpdateUser();
            throw new System.NotImplementedException();
        }
    }
}