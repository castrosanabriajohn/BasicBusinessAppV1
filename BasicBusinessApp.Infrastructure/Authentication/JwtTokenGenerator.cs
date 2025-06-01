using BasicBusinessApp.Application.Common.Interfaces.Authentication;
using BasicBusinessApp.Application.Common.Interfaces.Services;
using BasicBusinessApp.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicBusinessApp.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly IDateTimeProvider _dateTimeProvider;
  private readonly JwtSettings _jwtSettings;

  public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
  {
    _dateTimeProvider = dateTimeProvider;
    _jwtSettings = jwtSettings.Value;
  }

  public string GenerateToken(User user)
  {
    var signinCredentials = new SigningCredentials(
      new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
        SecurityAlgorithms.HmacSha256);
    var claims = new[]
    {
       new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
       new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
       new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var securityToken = new JwtSecurityToken(
      issuer: _jwtSettings.Issuer,
      claims: claims,
      signingCredentials: signinCredentials,
      expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
      audience: _jwtSettings.Audience
    );

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
