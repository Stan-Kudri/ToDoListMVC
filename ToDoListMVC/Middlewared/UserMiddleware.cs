using ToDoList.Core.Authentication;

namespace ToDoListMVC.Middlewared
{
    public class UserMiddleware
    {
        RequestDelegate _next;
        JwtTokenHelper _tokenHelper;

        public UserMiddleware(RequestDelegate next, JwtTokenHelper tokenHelper)
        {
            _next = next;
            _tokenHelper = tokenHelper;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Cookies.TryGetValue(LoginConst.GetTokenKey, out var token))
            {
                _tokenHelper.AuthUserIdGetByToken(token);
            }
        }
    }
}
