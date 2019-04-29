using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.Pipeline.ConsumerFactories;
using MassTransit.Scoping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.Catalog.Contracts.Events;
using Products.Search.Consumers;
using Products.Search.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Products.Search
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        var host = cfg.Host(Configuration.GetValue<string>("RabbitMq:Host"), "/", h => { });
                        cfg.ReceiveEndpoint(host, "Products.Search", e =>
                        {
                            e.PrefetchCount = 16;
                            e.UseMessageRetry(x => x.Interval(2, 100));

                            e.ConfigureConsumer<ProductUpdatedConsumer>(provider);

                            e.Batch<ProductUpdatedEvent>(b =>
                            {
                                b.MessageLimit = 3; // todo config
                                b.TimeLimit = TimeSpan.FromMinutes(5);
                                b.Consumer(() => new ProductUpdatedConsumer(Configuration));
                            });
                        });
                    }));
                c.AddConsumer<ProductUpdatedConsumer>();
            });

            services.AddSingleton<IHostedService, BusService>();

            services.AddTransient<ProductUpdatedConsumer>();

            services.AddTransient<IProductsService, ProductsService>(); // todo scoped or transient?
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    public class BusService :
        IHostedService
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
