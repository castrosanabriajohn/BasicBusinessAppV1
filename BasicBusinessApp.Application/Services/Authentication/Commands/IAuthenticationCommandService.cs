using BasicBusinessApp.Application.Common.Errors;
using BasicBusinessApp.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;

namespace BasicBusinessApp.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}
