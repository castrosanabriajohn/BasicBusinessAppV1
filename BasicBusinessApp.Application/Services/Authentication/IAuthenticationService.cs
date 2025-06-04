using BasicBusinessApp.Application.Common.Errors;
using ErrorOr;
using FluentResults;

namespace BasicBusinessApp.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}
