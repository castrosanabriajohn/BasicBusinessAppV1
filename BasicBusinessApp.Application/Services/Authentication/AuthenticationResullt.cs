using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);
