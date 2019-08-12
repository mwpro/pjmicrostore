﻿using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Cart.Consumers;
using Checkout.Cart.Domain;
using Checkout.Cart.Services;
using Common.Infrastructure;
using GreenPipes;
using Identity.Contracts;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHealthChecks()
                .AddDbContextCheck<CartContext>();

            services.SetupTokenValidation(Configuration);
            services.AddAuthorization(options => { options.AddRequireScopePolicy(Scopes.Carts); });

            services.AddDbContext<CartContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        var host = cfg.Host(Configuration.GetValue<string>("RabbitMq:Host"), "/", h => { });
                        cfg.ReceiveEndpoint(host, "Checkout.Cart", e =>
                        {
                            e.PrefetchCount = 16;
                            e.UseMessageRetry(x => x.Interval(2, 100));

                            e.ConfigureConsumer<OrderPlacedConsumer>(provider);
                            e.ConfigureConsumer<ProductUpdatedConsumer>(provider);
                        });
                    }));
                c.AddConsumer<OrderPlacedConsumer>();
                c.AddConsumer<ProductUpdatedConsumer>();
            });

            services.AddSingleton<IHostedService, BusService>();

            services.AddTransient<IProductsService, ProductsService>(); // todo scoped or transient?
            services.AddTransient<ProductUpdatedConsumer>();
            services.AddTransient<OrderPlacedConsumer>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/healthz");
            app.UseAuthentication();

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
