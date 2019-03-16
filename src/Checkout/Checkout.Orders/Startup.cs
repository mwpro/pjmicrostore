using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Orders.Domain;
using Checkout.Orders.Services;
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

            const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Checkout.Orders;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<OrdersContext>
                (options => options.UseSqlServer(connectionString));

            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        cfg.Host("localhost", "/", h => { });
                    }));
            });
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddTransient<ICartsService, CartsService>();
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
}
