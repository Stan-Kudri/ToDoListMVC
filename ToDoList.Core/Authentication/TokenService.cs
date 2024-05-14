using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoList.Core.Extension;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Authentication
{
    public class TokenService : ICurrentUserAccessor
    {
        private readonly AuthOptions _authOptions;

        public TokenService(IOptions<AuthOptions> authOptions)
            => _authOptions = authOptions.Value;

        public Guid? UserId { get; set; } = null;

        public string GenerateTokenJWT(User user)
        {
            var securityKey = _authOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var clainms = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("role", user.UserRole.ToString())
            };

            var token = new JwtSecurityToken(
                            issuer: _authOptions.Issuer,
                            audience: _authOptions.Audience,
                            claims: clainms,
                            expires: DateTime.UtcNow.AddHours(_authOptions.TokenLifeTime),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SetToken(string token)
        {
            var tokenHandler = token.GetTokenHandler(_authOptions);
            if (tokenHandler == null ||
                tokenHandler.ReadToken(token) is not JwtSecurityToken securityToken ||
                securityToken.ValidTo < DateTime.UtcNow ||
                securityToken.ValidFrom > DateTime.UtcNow)
            {
                return;
            }

            UserId = securityToken.Claims
                .Where(e => e.Type == ClaimTypes.NameIdentifier)
                .Select(e => !Guid.TryParse(e.Value, out var id) ? (Guid?)null : id)
                .FirstOrDefault();
        }
    }
}
