using BasicBusinessApp.Application.Common.Interfaces.Services;

namespace BasicBusinessApp.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow => DateTime.UtcNow;
}
