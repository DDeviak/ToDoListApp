using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListApp.BLL.Interfaces;
using ToDoListApp.BLL.Options;

namespace ToDoListApp.BLL.Services;
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly byte[] _key;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;

        ArgumentNullException.ThrowIfNull(_jwtSettings.Audience);
        ArgumentNullException.ThrowIfNull(_jwtSettings.Issuer);
        ArgumentNullException.ThrowIfNull(_jwtSettings.SigningKey);
        ArgumentNullException.ThrowIfNull(_jwtSettings.LifetimeInHours);

        _key = Encoding.UTF8.GetBytes(_jwtSettings.SigningKey!);
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public JwtSecurityToken GenerateToken(ClaimsIdentity claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.LifetimeInHours),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience?.FirstOrDefault(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature),
        };

        return _tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    }

    public string WriteToken(JwtSecurityToken token)
    {
        return _tokenHandler.WriteToken(token);
    }
}
