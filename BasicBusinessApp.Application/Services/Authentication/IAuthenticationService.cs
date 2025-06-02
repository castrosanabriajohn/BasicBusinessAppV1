using BasicBusinessApp.Application.Common.Errors;
using OneOf;

namespace BasicBusinessApp.Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
}
