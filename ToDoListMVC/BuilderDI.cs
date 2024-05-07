using ToDoList.Core.Authentication;
using ToDoList.Core.DBContext;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Repository;
using ToDoList.Core.Service;
using ToDoListMVC.Controllers;
using ToDoListMVC.Extension.ConfigJWTAuth;

namespace ToDoList
{
    public static class BuilderDI
    {
        public static void CreateDBContext(this WebApplicationBuilder builder, string path = "DataBase")
        {
            builder.Services.AddSingleton(e => new DbContextFactory(path));
            builder.Services.AddScoped(e => e.GetRequiredService<DbContextFactory>().Create());
            builder.Services.AddScoped<AffairsService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<JwtToken>();
            builder.Services.AddScoped<ICurrentUserAccessor>(e => e.GetRequiredService<JwtToken>());
            builder.Services.AddScoped(e => new User());
            builder.Services.AddScoped<AuthenticationController>();
            builder.Services.AddScoped<ToDoListController>();
        }

        public static void AddConfigureService(this WebApplicationBuilder builder)
        {
            var service = builder.Services;
            var configure = builder.Configuration;

            service.AddControllers();
            service.AddJWTBearerExtension(configure);
            service.AddCorsOptions();
        }
    }
}
