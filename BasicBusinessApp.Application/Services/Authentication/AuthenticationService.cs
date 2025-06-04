using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Common.Interfaces.Authentication;
using BasicBusinessApp.Application.Common.Interfaces.Persistence;
using BasicBusinessApp.Common.Errors;
using BasicBusinessApp.Domain.Common;
using BasicBusinessApp.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace BasicBusinessApp.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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
  public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
  {
    // check if user alreay exists
    if(_userRepository.GetUserByEmail(email) is not null) {
      return Errors.User.DuplicateEmail;
    }
    // create user and generate unique id
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);
    // create jwt token
    var token = _jwtTokenGenerator.GenerateToken(user);
    return new AuthenticationResult(user, token);
  }
}