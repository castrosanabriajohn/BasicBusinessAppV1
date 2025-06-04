using BasicBusinessApp.Domain.Common;
using BasicBusinessApp.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BasicBusinessApp.Application.Authentication.Common;
using BasicBusinessApp.Application.Authentication.Commands.Register;
using BasicBusinessApp.Application.Authentication.Queries.Login;
using MapsterMapper;

namespace BasicBusinessApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mappper;

  public AuthenticationController(ISender mediator, IMapper mappper)
  {
    _mediator = mediator;
    _mappper = mappper;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = _mappper.Map<RegisterCommand>(request);
    ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);
    return registerResult.Match(
      registerResult => Ok(_mappper.Map<AuthenticationResponse>(registerResult)),
      errors => Problem(errors)
    );
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = _mappper.Map<LoginQuery>(request);
    var authResult = await _mediator.Send(query);
    
    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description
      );
    }
    return authResult.Match(
        authResult => Ok(_mappper.Map<AuthenticationResponse>(authResult)),
        errors => Problem(errors)
      );
  }
}
