using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}
