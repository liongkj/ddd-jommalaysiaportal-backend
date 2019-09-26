using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.UserUseCase.Delete
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserRequest message, IOutputPort<DeleteUserResponse> outputPort)
        {
            var DeleteUserResponse = await _userRepository.DeleteUser(message.Userid);
            throw new System.NotImplementedException();
        }
    }
}