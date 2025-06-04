using System.Reflection;
using BasicBusinessApp.Application.Authentication.Commands.Register;
using BasicBusinessApp.Application.Authentication.Common;
using BasicBusinessApp.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BasicBusinessApp.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, ValidateRegisterCommandBehavior>();
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    return services;
  }
}
