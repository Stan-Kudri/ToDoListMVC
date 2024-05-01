﻿using ToDoList.Core.Authentication;
using ToDoList.Core.DBContext;
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
            builder.Services.AddScoped<JwtTokenHelper>();
            builder.Services.AddScoped(e => e.GetRequiredService<UserService>().GetUser("StanKudri", "$2a$11$/JFnSuwAVPjEeuin1DQveuF2AdXa/NNSfzMJq35/mCbk9ROLM.laS"));
            builder.Services.AddScoped<AuthenticationController>();
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
