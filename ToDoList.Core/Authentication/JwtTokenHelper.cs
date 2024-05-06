using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoList.Core.Extension;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Authentication
{
    public class JwtTokenHelper : ICurrentUserAccessor
    {
        private readonly AuthOptions _authOptions;

        public JwtTokenHelper(IOptions<AuthOptions> authOptions)
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
                            expires: DateTime.Now.AddHours(_authOptions.TokenLifeTime),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsUserIdGetByToken(string token)
        {
            var tokenHandler = token.GetTokenHandler(_authOptions);
            if (tokenHandler != null && tokenHandler.ReadToken(token) is JwtSecurityToken securityToken)
            {
                var claims = securityToken.Claims.ToList();
                var strId = claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
                UserId = !Guid.TryParse(strId, out var id) ? null : id;
                return UserId != null;
            }

            return false;
        }

        public void AuthUserIdGetByToken(string token)
        {
            var tokenHandler = token.GetTokenHandler(_authOptions);
            if (tokenHandler != null && tokenHandler.ReadToken(token) is JwtSecurityToken securityToken)
            {
                var claims = securityToken.Claims.ToList();
                var strId = claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
                UserId = !Guid.TryParse(strId, out var id) ? null : id;
            }
        }
    }
}
