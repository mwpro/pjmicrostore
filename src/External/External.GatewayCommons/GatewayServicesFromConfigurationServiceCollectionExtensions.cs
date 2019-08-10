﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.ServiceDiscovery;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace External.GatewayCommons
{
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