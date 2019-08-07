using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure
{
    public static class ServiceCollectionTokenServiceExtensions
    {
        public static void SetupTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationTokenService, AuthorizationTokenService>();
        }
    }
}