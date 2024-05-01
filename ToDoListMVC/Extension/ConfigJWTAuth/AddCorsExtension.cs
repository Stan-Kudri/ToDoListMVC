namespace ToDoListMVC.Extension.ConfigJWTAuth
{
    public static class AddCorsExtension
    {
        public static void AddCorsOptions(this IServiceCollection service)
            => service.AddCors(options =>
               {
                   options.AddDefaultPolicy(
                       builder =>
                           builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
               });
    }
}
