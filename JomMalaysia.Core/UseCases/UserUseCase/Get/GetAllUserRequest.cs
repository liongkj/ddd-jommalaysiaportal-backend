using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetUserRequest : IUseCaseRequest<GetUserResponse>
    {
        public string UserId { get; set; }

        public GetUserRequest(string userId)
        {
            UserId = userId;
        }
    }
}
