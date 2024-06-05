using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToDoList.Core.Authentication;

namespace ToDoListMVC.Extension.ConfigJWTAuth
{
    public static class AddAuthenticationExtension
    {
        public static void AddJWTBearerExtension(this IServiceCollection service, IConfiguration configure)
        {
            var section = configure.GetSection("Auth");
            service.Configure<AuthOptions>(section);
            var authOptions = section.Get<AuthOptions>();

            // Disable BCC2008
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions?.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions?.Audience,

                        ValidateLifetime = true,

                        //HS256
                        IssuerSigningKey = authOptions?.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies[TokensConst.GetTokenKey];
                            context.Token = token;
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
