using ToDoListMVC.Models;

namespace ToDoListMVC.Extension
{
    public static class TokenValidatorExtension
    {
        public static TokenValidator GetTokenValidator(this HttpContext httpContext, string acsessToken, string refreshToken)
        {
            var tokenValidator = httpContext.RequestServices.GetRequiredService<TokenValidator>();
            tokenValidator.InitializingParameters(httpContext, acsessToken, refreshToken);
            return tokenValidator;
        }
    }
}
