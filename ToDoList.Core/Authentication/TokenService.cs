using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                            expires: DateTime.UtcNow.AddMinutes(_authOptions.TokenLifeTime),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomNumber),
                Create = LoginConst.GetDateCreateRefreshToken,
                Expires = LoginConst.GetExpiresRefreshToken,
            };
        }

        public void SetAcsessToken(string token)
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

        public bool ValidAcsessToken(string token)
        {
            var tokenHandler = token.GetTokenHandler(_authOptions);
            return tokenHandler != null &&
                tokenHandler.ReadToken(token) is JwtSecurityToken securityToken &&
                securityToken.ValidTo >= DateTime.UtcNow &&
                securityToken.ValidFrom <= DateTime.UtcNow;
        }

        public bool ShouldUppdateAcsessToken(string token)
        {
            var tokenHandler = token.GetTokenHandler(_authOptions);

            if (tokenHandler == null || tokenHandler.ReadToken(token) is not JwtSecurityToken securityToken)
            {
                throw new ArgumentException("Token is not valid.");
            }

            return DateTime.UtcNow >= securityToken.ValidFrom.Add(LoginConst.GetUpdateTimeToken) && DateTime.UtcNow <= securityToken.ValidTo;
        }
    }
}
