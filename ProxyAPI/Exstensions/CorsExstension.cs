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
                        .WithOrigins("https://localhost:3000") // change to match frontend port when frontend is available (default for react or next.js is usually 3000 though)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
