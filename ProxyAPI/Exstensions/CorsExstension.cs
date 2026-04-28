namespace ProxyAPI.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("StrictPolicy", policy =>
                {
                    policy
                        .WithOrigins("https://localhost:3000") // behöver ändras senare
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}