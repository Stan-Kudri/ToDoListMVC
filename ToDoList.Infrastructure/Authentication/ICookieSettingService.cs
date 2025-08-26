using Microsoft.AspNetCore.Http;
using ToDoList.Core.Models.Users;

namespace ToDoList.Infrastructure.Authentication
{
    public interface ICookieSettingService
    {
        void SetTokens(HttpContext httpContext, User user);
        void RemoveTokens(HttpContext httpContext, string refreshToken);
    }
}
