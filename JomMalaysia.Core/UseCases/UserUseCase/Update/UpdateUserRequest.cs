using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.UserUseCase.Update
{
    public class UpdateUserRequest : IUseCaseRequest<UpdateUserResponse>
    {

        public string UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}