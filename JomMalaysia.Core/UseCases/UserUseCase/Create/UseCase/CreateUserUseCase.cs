using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Framework.Configuration;
using System;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create.UseCase
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IAppSetting _appSetting;

        public CreateUserUseCase(
            IUserRepository userRepository,
            ILoginInfoProvider loginInfo,
            IAppSetting appSetting)
        {
            _userRepository = userRepository;
            _loginInfo = loginInfo;
            _appSetting = appSetting;
        }

        public bool Handle(CreateUserRequest message, IOutputPort<CreateUserResponse> outputPort)
        {
            //validate request
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.user.VerifyEmail = true;
            message.user.Connection = _appSetting.DBConnection;
            message.user.Password = RandomPassword();

            var response = _userRepository.CreateUser(message.user);

            return true;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}
