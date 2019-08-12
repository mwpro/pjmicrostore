using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using External.GatewayCommons;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                    s.AddHealthChecks();
                    s.AddOcelot();
                    s.AddCors(options => options.AddDefaultPolicy(new CorsPolicy()
                    {
                        // todo
                        Origins = {"*"},
                        Methods = {"*"},
                        Headers = {"*"}
                    }));
                    s.AddGatewayServicesFromConfiguration();
                })
                .Configure(app =>
                {
                    app.UseHealthChecks("/healthz");
                    app.UseCors().UseOcelot().Wait();
                });
    }
}
