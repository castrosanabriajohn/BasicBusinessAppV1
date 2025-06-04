using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Common.Interfaces.Authentication;
using BasicBusinessApp.Application.Common.Interfaces.Persistence;
using BasicBusinessApp.Application.Services.Authentication.Common;
using BasicBusinessApp.Common.Errors;
using BasicBusinessApp.Domain.Common;
using BasicBusinessApp.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace BasicBusinessApp.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public ErrorOr<AuthenticationResult> Login(string email, string password)
  {
    // validate user exists
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }
    // check password
    if (user.Password != password)
    {
      return new[] { Errors.Authentication.InvalidCredentials };
    }
    // create jwt token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user,token);
  }
}