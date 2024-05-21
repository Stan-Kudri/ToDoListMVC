using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using ToDoList;
using ToDoList.Core.Authentication;
using ToDoList.Core.Extension;
using ToDoList.Core.Service;
using ToDoListMVC.Extension;

var builder = WebApplication.CreateBuilder(args);
StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.AddConfigureService();
builder.CreateDBContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.Use((context, func) =>
{
    if (!context.Request.Cookies.TryGetValue(LoginConst.GetTokenKey, out var usingToken)
        || !context.Request.Cookies.TryGetValue(LoginConst.GetRefreshTokenKey, out var usingRefreshToken))
    {
        return RedirectIfNeeded(context, func);
    }

    var tokenHelper = context.RequestServices.GetRequiredService<TokenService>();
    tokenHelper.SetToken(usingToken);
    var userId = tokenHelper.UserId;

    if (userId == null)
    {
        return RedirectIfNeeded(context, func);
    }

    var refreshTokenServer = context.RequestServices.GetRequiredService<RefreshTokenService>();
    var refreshToken = refreshTokenServer.GetRefreshToken(usingRefreshToken, (Guid)userId);

    if (refreshToken == null || refreshTokenServer.IsExistRefreshToken(refreshToken))
    {
        return RedirectIfNeeded(context, func);
    }

    if (!refreshToken.IsActiveRefreshToken() || refreshTokenServer.IsExistRefreshToken(refreshToken))
    {
        context.Response.Cookies.Delete(LoginConst.GetTokenKey);
    }
    else if (!refreshToken.IsExpiredRefreshToken() && tokenHelper.UserId != null)
    {
        var userService = context.RequestServices.GetRequiredService<UserService>();
        var newRefreshToken = tokenHelper.GenerateRefreshToken(userService.GetUser(userId));
        refreshTokenServer.Uppdata(newRefreshToken);
        context.AppendRefreshToken(newRefreshToken.Token);
    }

    return tokenHelper.UserId == null || refreshTokenServer.IsExistRefreshToken(refreshToken) ? RedirectIfNeeded(context, func) : func();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=SignIn}/{id?}");

app.Run();

Task RedirectIfNeeded(HttpContext context, Func<Task> func)
{
    var endpoint = context.GetEndpoint();

    if (endpoint != null && endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
    {
        return func();
    }

    context.Response.Redirect("/Authentication/SignIn");
    return Task.CompletedTask;
}