﻿using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Customer);
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Delivery);
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Payment);
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.BillingAddress);
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.ShippingAddress);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}