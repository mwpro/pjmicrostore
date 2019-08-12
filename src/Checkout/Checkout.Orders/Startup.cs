using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Consumers;
using Checkout.Orders.Domain;
using Checkout.Orders.Infrastructure;
using Checkout.Orders.Services;
using Common.Infrastructure;
using GreenPipes;
using Identity.Contracts;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Checkout.Orders
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

            services.AddHealthChecks()
                .AddDbContextCheck<OrdersContext>();

            services.SetupTokenValidation(Configuration);
            services.SetupTokenService(Configuration);
            services.AddAuthorization(options =>
            {
                options.AddAdminOnlyPolicy();
            });

            services.AddDbContext<OrdersContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        var host = cfg.Host(Configuration.GetValue<string>("RabbitMq:Host"), "/", h => { });
                        cfg.ReceiveEndpoint(host, "Checkout.Orders", e =>
                        {
                            e.PrefetchCount = 16;
                            e.UseMessageRetry(x => x.Interval(2, 100));

                            e.ConfigureConsumer<PaymentCompletedEventConsumer>(provider);
                            e.ConfigureConsumer<PaymentCreatedEventConsumer>(provider);
                        });
                    }));

                c.AddConsumer<PaymentCompletedEventConsumer>();
                c.AddConsumer<PaymentCreatedEventConsumer>();
            });

            services.AddSingleton<IHostedService, BusService>();

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IShippingService, ShippingService>();
            services.AddTransient<IDatabase, SqlDatabase>(provider => new SqlDatabase(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<PaymentCompletedEventConsumer>();
            services.AddTransient<PaymentCreatedEventConsumer>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
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
