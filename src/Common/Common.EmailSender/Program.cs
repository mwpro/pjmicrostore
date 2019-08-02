using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common.EmailSender.Consumers;
using Common.EmailSender.Services;
using FluentEmail.Core;
using FluentEmail.Core.Defaults;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.EmailSender
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureLogging((hostContext, config) =>
                {
                    config.AddConsole();
                })
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddMassTransit(c =>
                    {
                        c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                            cfg =>
                            {
                                var rmqHost = cfg.Host(hostContext.Configuration.GetValue<string>("RabbitMq:Host"), "/", h => { });
                                cfg.ReceiveEndpoint(rmqHost, "Common.EmailSender", e =>
                                {
                                    e.PrefetchCount = 16;
                                    e.UseMessageRetry(x => x.Interval(2, 100));

                                    e.ConfigureConsumer<OrderPlacedConsumer>(provider);
                                });
                            }));

                        c.AddConsumer<OrderPlacedConsumer>();
                    });

                    Email.DefaultSender = new CustomSaveToDiskSender($"{Directory.GetCurrentDirectory()}");
                    Email.DefaultRenderer = new CustomRazorRenderer($"{Directory.GetCurrentDirectory()}/Templates/");

                    services.AddSingleton<IHostedService, BusService>();
                })
                .UseConsoleLifetime()
                .Build();

            using (host)
            {
                // Start the host
                await host.StartAsync();

                // Wait for the host to shutdown
                await host.WaitForShutdownAsync();
            }
        }
    }

    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;

        public BusService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
