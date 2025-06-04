using BasicBusinessApp.Application.Authentication.Commands.Register;
using BasicBusinessApp.Application.Authentication.Common;
using BasicBusinessApp.Application.Authentication.Queries.Login;
using BasicBusinessApp.Contracts.Authentication;
using Mapster;

namespace BasicBusinessApp.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterCommand>();
    config.NewConfig<LoginRequest, LoginQuery>();
    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
      .Map(dest => dest.Token, src => src.Token)
      .Map(dest => dest, src => src.User);
  }
}
