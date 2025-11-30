using Microsoft.AspNetCore.Cors.Infrastructure;

namespace MyPortfolio.Utility
{
    public static class ServiceFactory
    {
        internal static void ConfigureCors(IConfiguration configuration, CorsOptions options)
        {
            // Leggi origin dal file di configurazione
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

            options.AddPolicy(name: "AllowAll", cfg =>
            {
                cfg.SetIsOriginAllowed(origin =>
                {
                    if (string.IsNullOrEmpty(origin))
                        return false;

                    var host = new Uri(origin).Host;

                    // Permetti origin configurati + tutti i domini Tailscale + localhost
                    return allowedOrigins.Contains(origin) || host.EndsWith(".ts.net") || host == "localhost" || host.Contains("192.168.1");
                })
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        }
    }
}
