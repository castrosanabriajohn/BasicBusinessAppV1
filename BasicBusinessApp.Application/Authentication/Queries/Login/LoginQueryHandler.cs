using BasicBusinessApp.Application.Common.Interfaces.Authentication;
using BasicBusinessApp.Application.Common.Interfaces.Persistence;
using BasicBusinessApp.Domain.Entities;
using BasicBusinessApp.Domain.Common;
using ErrorOr;
using MediatR;
using BasicBusinessApp.Application.Authentication.Common;

namespace BasicBusinessApp.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
    // validate user exists
    if (_userRepository.GetUserByEmail(query.Email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }
    // check password
    if (user.Password != query.Password)
    {
      return new[] { Errors.Authentication.InvalidCredentials };
    }
    // create jwt token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user,token);
  }
}