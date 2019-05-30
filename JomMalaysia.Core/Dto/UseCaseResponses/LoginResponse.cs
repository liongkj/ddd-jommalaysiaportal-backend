using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Dto.UseCaseResponses
{
  public class LoginResponse : UsecaseResponseMessage
  {
    public Token Token { get; }
    public IEnumerable<Error> Errors { get; }

    public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
    {
      Errors = errors;
    }

    public LoginResponse(Token token, bool success = false, string message = null) : base(success, message)
    {
      Token = token;
    }
  }
}
