using BasicBusinessApp.Application.Common.Interfaces.Authentication;

namespace BasicBusinessApp.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  public readonly IJwtTokenGenerator _jwtTokenGenerator;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) => _jwtTokenGenerator = jwtTokenGenerator;

  public AuthenticationResult Login(string email, string password)
  {
    return new AuthenticationResult(Guid.NewGuid(), "firstName", "lastName", email, "token");
  }
  public AuthenticationResult Register(string firstName, string lastName, string email, string password)
  {
    // check if user alreay exists

    // create user and generate unique id

    // create jwt token
    var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), "firstName", "lastName");
    Guid userId = Guid.NewGuid();
    return new AuthenticationResult(userId, firstName, lastName, email, token);
  }
}