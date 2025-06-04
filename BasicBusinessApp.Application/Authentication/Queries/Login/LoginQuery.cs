using MediatR;
using ErrorOr;
using BasicBusinessApp.Application.Authentication.Common;

namespace BasicBusinessApp.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
