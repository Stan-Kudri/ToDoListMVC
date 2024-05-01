using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Authentication
{
    public class JwtTokenHelper
    {
        private readonly IOptions<AuthOptions> _authOptions;

        public JwtTokenHelper(IOptions<AuthOptions> authOptions)
            => _authOptions = authOptions;

        public string GenerateTokenJWT(User user)
        {
            var authOptions = _authOptions.Value;

            var securityKey = authOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var clainms = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString(), ClaimValueTypes.String)
            };

            var token = new JwtSecurityToken(
                            authOptions.Issuer,
                            authOptions.Audience,
                            clainms,
                            expires: DateTime.Now.AddHours(authOptions.TokenLifeTime),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
