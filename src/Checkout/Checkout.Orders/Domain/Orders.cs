using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

    public class Order
    {
        public int Id { get; set; }
        // todo fancy order number like 1234/07/2018
        public DateTime CreateDate { get; set; } // todo rename to createDate utc
        public string Status { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal Total => OrderLines.Sum(x => x.Value);
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Value => ProductPrice * Quantity;
    }
}
