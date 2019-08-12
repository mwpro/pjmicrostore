using System;
using System.Threading;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Products.Photos.Domain;
using Products.Photos.Storage;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Products.Photos
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // todo hmm, probably it only hides the problem
                });

            services.AddHealthChecks()
                .AddDbContextCheck<PhotosContext>();

            services.SetupTokenValidation(Configuration);
            services.AddAuthorization(options => { options.AddAdminOnlyPolicy(); });

            services.AddDbContext<PhotosContext>
                    (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddMassTransit(c =>
            {
                c.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                    {
                        var host = cfg.Host(Configuration.GetValue<string>("RabbitMq:Host"), "/", h => { });
                        cfg.ReceiveEndpoint(host, "Products.Photos", e =>
                        {
                            e.PrefetchCount = 16;
                            e.UseMessageRetry(x => x.Interval(2, 100));
                        });
                    }));
            });

            services.AddSingleton<IHostedService, BusService>();
            services.AddSingleton<IPhotoStorage, AzurePhotoStorage>();
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
