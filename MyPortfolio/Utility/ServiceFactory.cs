using Microsoft.AspNetCore.Cors.Infrastructure;

namespace MyPortfolio.Utility
{
    public static class ServiceFactory
    {
        internal static void ConfigureCors(IConfiguration configuration, CorsOptions options)
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            if (allowedOrigins is null)
            {
                return;
            }
            options.AddPolicy(name: "AllowAll", cfg =>
            {
                cfg.WithOrigins(allowedOrigins).AllowCredentials();
                cfg.AllowAnyMethod();
                cfg.AllowAnyHeader();
            });
        }
    }
}
