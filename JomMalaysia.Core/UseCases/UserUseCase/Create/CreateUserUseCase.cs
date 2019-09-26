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

            User NewUser = new User(message.Username, message.Email, message.Name);
            var response = await _userRepository.CreateUser(NewUser);
            outputPort.Handle(response);
            return response.Success;
        }

        //     public int RandomNumber(int min, int max)
        //     {
        //         Random random = new Random();
        //         return random.Next(min, max);
        //     }

        //     // Generate a random string with a given size    
        //     public string RandomString(int size, bool lowerCase)
        //     {
        //         StringBuilder builder = new StringBuilder();
        //         Random random = new Random();
        //         char ch;
        //         for (int i = 0; i < size; i++)
        //         {
        //             ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //             builder.Append(ch);
        //         }
        //         if (lowerCase)
        //             return builder.ToString().ToLower();
        //         return builder.ToString();
        //     }

        //     // Generate a random password    
        //     public string RandomPassword()
        //     {
        //         StringBuilder builder = new StringBuilder();
        //         builder.Append(RandomString(4, true));
        //         builder.Append(RandomNumber(1000, 9999));
        //         builder.Append(RandomString(2, false));
        //         return builder.ToString();
        //     }
    }
}
