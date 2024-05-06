using ToDoList.Core.Authentication;

namespace ToDoListMVC.Extension.ConfigJWTAuth
{
    public static class GetTokenByContextExtension
    {
        public static string GetToken(this HttpContext httpContext)
        {
            return !httpContext.Request.Cookies.TryGetValue(LoginConst.GetTokenKey, out var token) ? token : string.Empty;
        }
    }
}
