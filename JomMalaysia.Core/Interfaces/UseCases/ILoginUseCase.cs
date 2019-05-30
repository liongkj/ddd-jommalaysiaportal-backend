using JomMalaysia.Core.Dto.UseCaseRequests;
using JomMalaysia.Core.Dto.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces.UseCases
{
  public interface ILoginUseCase : IUseCaseRequestHandler<LoginRequest, LoginResponse>
  {
  }
}
