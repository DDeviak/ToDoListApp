using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ToDoListApp.BLL.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateToken(ClaimsIdentity claims);

        public string WriteToken(JwtSecurityToken token);
    }
}