using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Common.Interfaces.Authentication;
using BasicBusinessApp.Application.Common.Interfaces.Persistence;
using BasicBusinessApp.Common.Errors;
using BasicBusinessApp.Domain.Entities;
using OneOf;

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

  public AuthenticationResult Login(string email, string password)
  {
    // validate user exists
    if(_userRepository.GetUserByEmail(email) is not User user) {
      throw new Exception("User with given email does not exist");
    }
    // check password
    if (user.Password != password)
    {
      throw new Exception("Invalid password");
    }
    // create jwt token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user,token);
  }
  public OneOf<AuthenticationResult, DuplicateEmailError> Register(string firstName, string lastName, string email, string password)
  {
    // check if user alreay exists
    if(_userRepository.GetUserByEmail(email) is not null) {
      return new DuplicateEmailError();
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