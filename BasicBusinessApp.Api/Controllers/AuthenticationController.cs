using BasicBusinessApp.Domain.Common;
using BasicBusinessApp.Application.Services.Authentication;
using BasicBusinessApp.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BasicBusinessApp.Application.Services.Authentication.Common;
using BasicBusinessApp.Application.Services.Authentication.Commands;
using BasicBusinessApp.Application.Services.Authentication.Queries;

namespace BasicBusinessApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
  private readonly IAuthenticationCommandService _authenticationCommandService;
  private readonly IAuthenticationQueryService _authenticationQueryService;
  public AuthenticationController(
    IAuthenticationCommandService authenticationCommandService,
    IAuthenticationQueryService authenticationQueryService)
  {
    _authenticationCommandService = authenticationCommandService;
    _authenticationQueryService = authenticationQueryService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    ErrorOr<AuthenticationResult> registerResult = _authenticationCommandService.Register(
     request.FirstName,
     request.LastName,
     request.Email,
     request.Password);
    return registerResult.Match(
      registerResult => Ok(MapAuthResult(registerResult)),
      errors => Problem(errors)
    );
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationQueryService.Login(
      request.Email,
      request.Password
    );
    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    { 
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description
      );
    }
    return authResult.Match(
        authResult => Ok(MapAuthResult(authResult)),
        errors => Problem(errors)
      );
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
