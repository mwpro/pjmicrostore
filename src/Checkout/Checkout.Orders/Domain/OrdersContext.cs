using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Checkout.Orders.Domain
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) }));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Status);

            //modelBuilder.Entity<AttributeValue>()
            //    .HasOne(x => x.Product)
            //    .WithMany(x => x.Attributes)
            //    .HasForeignKey(x => x.ProductId);

            //modelBuilder.Entity<AttributeValue>()
            //    .HasOne(x => x.Attribute)
            //    .WithMany()
            //    .HasForeignKey(x => x.AttributeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}