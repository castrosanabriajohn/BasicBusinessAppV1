using BasicBusinessApp.Api.Common.Errors;
using BasicBusinessApp.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BasicBusinessApp.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddMappings();
    services.AddControllers();
    services.AddSingleton<ProblemDetailsFactory, DetailsFactory>();
    return services;
  }
}
