using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);
}
