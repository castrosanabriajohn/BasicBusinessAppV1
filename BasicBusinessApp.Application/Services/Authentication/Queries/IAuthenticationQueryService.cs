using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;

namespace BasicBusinessApp.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}
