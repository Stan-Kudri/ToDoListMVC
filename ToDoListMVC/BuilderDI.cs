using ToDoList.Core.DBContext;
using ToDoList.Core.Repository;
using ToDoList.Core.Service;

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
            builder.Services.AddScoped(e => e.GetRequiredService<UserService>().GetUser("StanKudri", "$2a$11$/JFnSuwAVPjEeuin1DQveuF2AdXa/NNSfzMJq35/mCbk9ROLM.laS"));
        }
    }
}
