using MediatR;
using ErrorOr;
using BasicBusinessApp.Application.Authentication.Common;

namespace BasicBusinessApp.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
