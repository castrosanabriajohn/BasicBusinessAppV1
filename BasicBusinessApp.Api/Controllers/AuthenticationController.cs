using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Services.Authentication;
using BasicBusinessApp.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BasicBusinessApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
  private readonly IAuthenticationService _authenticationService;
  public AuthenticationController(IAuthenticationService authenticationService) => _authenticationService = authenticationService;

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    var registerResult = _authenticationService.Register(
     request.FirstName,
     request.LastName,
     request.Email,
     request.Password);
    if (registerResult.IsSuccess)
    {
      return Ok(MapAuthResult(registerResult.Value));
    }
    var firstError = registerResult.Errors[0];

    if (firstError is DuplicateEmailError)
    {
      return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists");
    }
    return Problem();
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationService.Login(
      request.Email,
      request.Password
    );
    var response = new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
    return Ok(response);
  }
  private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult) {
    return new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
  }
}
