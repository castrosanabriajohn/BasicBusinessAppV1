using BasicBusinessApp.Application.Common.Interfaces.Persistence;
using BasicBusinessApp.Domain.Entities;

namespace BasicBusinessApp.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
  private static readonly List<User> users = [];

  public void Add(User user)
  {
    users.Add(user);
  }
  public User? GetUserByEmail(string email) => users.SingleOrDefault(u => u.Email == email);
}
