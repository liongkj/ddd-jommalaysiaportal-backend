using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Dto.UseCaseRequests;
using JomMalaysia.Core.Dto.UseCaseResponses;
using JomMalaysia.Core.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Gateways.Repositories;
using JomMalaysia.Core.Interfaces.UseCases;

namespace JomMalaysia.Core.UseCases
{
    public sealed class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterUserRequest message, IOutputPort<RegisterUserResponse> outputPort)
        {
            var response = await _userRepository.Create(new User(message.FirstName, message.LastName,message.Email, message.UserName), message.Password);
            outputPort.Handle(response.Success ? new RegisterUserResponse(response.Id, true) : new RegisterUserResponse(response.Errors.Select(e => e.Description)));
            return response.Success;
        }
    }
}
