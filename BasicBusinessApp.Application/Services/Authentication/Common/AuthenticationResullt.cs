using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);
