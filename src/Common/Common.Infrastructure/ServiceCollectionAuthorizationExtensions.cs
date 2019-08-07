using System;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure
{
    public static class ServiceCollectionAuthorizationExtensions
    {
        public static void SetupTokenValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration.GetValue<string>("Identity:Authority");
                    options.RequireHttpsMetadata = configuration.GetValue<bool>("Identity:RequireHttpsMetadata");
                    options.JwtValidationClockSkew =
                        TimeSpan.FromSeconds(configuration.GetValue<int>("Identity:JwtValidationClockSkewSeconds"));
                });
        }
    }
}
