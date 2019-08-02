using System.Threading;
using System.Threading.Tasks;
using Common.EmailSender.Infrastructure;
using Common.EmailSender.Orders;
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

                    services.AddEmailServices(hostContext.Configuration);

                    services.AddSingleton<IHostedService, BusService>();
                    services.AddTransient<ISendMailService, SendMailService>();

                    services.AddTransient<IOrdersService, OrdersService>();
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
