using JomMalaysia.Core.Dto.UseCaseResponses;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Dto.UseCaseRequests
{
  public class LoginRequest : IUseCaseRequest<LoginResponse>
  {
    public string Username { get; }
    public string Password { get; }

    public LoginRequest(string username, string password)
    {
      Username = username;
      Password = password;
    }
  }
}
