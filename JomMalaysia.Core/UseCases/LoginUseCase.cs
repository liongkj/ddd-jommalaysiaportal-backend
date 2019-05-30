using System.Threading.Tasks;
using JomMalaysia.Core.Dto;
using JomMalaysia.Core.Dto.UseCaseRequests;
using JomMalaysia.Core.Dto.UseCaseResponses;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Gateways.Repositories;
using JomMalaysia.Core.Interfaces.Services;
using JomMalaysia.Core.Interfaces.UseCases;

namespace JomMalaysia.Core.UseCases
{
    public sealed class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;

        public LoginUseCase(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Username) && !string.IsNullOrEmpty(message.Password))
            {
                // confirm we have a user with the given name
                var user = await _userRepository.FindByName(message.Username);
                if (user != null)
                {
                    // validate password
                    if (await _userRepository.CheckPassword(user, message.Password))
                    {
                        // generate token
                        outputPort.Handle(new LoginResponse(await _jwtFactory.GenerateEncodedToken(user.Id, user.Username),true));
                        return true;
                    }
                }
            }
            outputPort.Handle(new LoginResponse(new[] { new Error("login_failure", "Invalid username or password.") }));
            return false;
        }
    }
}
