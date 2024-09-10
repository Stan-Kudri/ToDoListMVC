using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoList.Core.Authentication;

namespace ToDoList.Core.Extension
{
    public static class TokenHandlerExtension
    {
        public static JwtSecurityTokenHandler? GetTokenHandler(this string token, AuthOptions authOptions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions?.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions?.Audience,

                    ValidateLifetime = true,

                    //HS256
                    IssuerSigningKey = authOptions?.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken validatedToken);


                return tokenHandler;
            }
            catch
            {
                return null;
            }
        }
    }
}
