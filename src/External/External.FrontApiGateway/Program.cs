using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.ServiceDiscovery;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace External.FrontApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                    {
                        builder
                            .AddJsonFile(Path.Combine("Configuration", "gateway.json"), false, true)
                            .AddJsonFile(Path.Combine("Configuration", $"gateway.{hostingContext.HostingEnvironment.EnvironmentName}.json"), true, true)
                            .AddEnvironmentVariables();
                    })
                .ConfigureServices((webHost, s) =>
                {
                    s.AddOcelot();
                    s.AddGatewayServicesFromConfiguration();
                })
                .Configure(app =>
                {
                    app.UseOcelot().Wait();
                });
    }

    public class GatewayServicesConfiguration
    {
        public Dictionary<string, GatewayServiceConfiguration> GatewayServices { get; set; }

        public class GatewayServiceConfiguration
        {
            public string DownstreamHost { get; set; }
            public int DownstreamPort { get; set; }
        }
    }

    public static class GatewayServicesFromConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddGatewayServicesFromConfiguration(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)service.ImplementationInstance;

            var servicesConfiguration = configuration.Get<GatewayServicesConfiguration>();
            var gatewayServices = servicesConfiguration.GatewayServices.Select(x =>
                    new Service(x.Key.ToLower(), new ServiceHostAndPort(x.Value.DownstreamHost, x.Value.DownstreamPort), string.Empty,
                        string.Empty, new string[0])).ToList();
            Console.WriteLine(String.Join(Environment.NewLine, gatewayServices.Select(x => $"Configured service: {x.Name}@{x.HostAndPort.DownstreamHost}:{x.HostAndPort.DownstreamPort}")));
            services.AddSingleton<ServiceDiscoveryFinderDelegate>((provider, config, key) => new ConfigurationServiceProvider(gatewayServices
                .Where(s => s.Name.Equals(key)).ToList()));
            return services;
        }
    }
}
