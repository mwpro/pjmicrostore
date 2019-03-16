using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Cart.Consumers;
using Checkout.Cart.Domain;
using Checkout.Cart.Services;
using Checkout.Orders.Controllers;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Checkout.Cart
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Checkout.Cart;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<CartContext>
                (options => options.UseSqlServer(connectionString));

            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        cfg.Host("localhost", "/", h => { });
                        cfg.ReceiveEndpoint("Checkout.Cart", e =>
                        {
                            //e.PrefetchCount = 16;
                            e.UseMessageRetry(x => x.Interval(2, 100));

                            e.ConfigureConsumer<OrderPlacedConsumer>(provider);

                            EndpointConvention.Map<OrderPlacedEvent>(e.InputAddress);
                        });
                    }));
                c.AddConsumer<OrderPlacedConsumer>();
            });
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddTransient<IProductsService, ProductsService>(); // todo scoped or transient?
            services.AddTransient<OrderPlacedConsumer>();
            services.AddSingleton<IHostedService, CartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public class CartService :
        IHostedService
    {
        private readonly IBusControl _busControl;

        public CartService(IBusControl busControl)
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
