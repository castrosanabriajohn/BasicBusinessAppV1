using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);