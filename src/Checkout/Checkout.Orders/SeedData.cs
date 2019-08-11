using Checkout.Orders.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MigrationsEnsurer
{
    public class SeedData
    {
        public static void EnsureMigrations(IWebHost host)
        {
            var config = host.Services.GetRequiredService<IConfiguration>();
            var services = new ServiceCollection();
            services.AddDbContext<OrdersContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var ordersContext = scope.ServiceProvider.GetRequiredService<OrdersContext>();
                    ordersContext.Database.Migrate();
                }
            }
        }
    }
}