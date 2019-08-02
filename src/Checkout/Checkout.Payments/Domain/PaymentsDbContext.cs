using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Checkout.Payments.Domain
{
    public class PaymentsDbContext : DbContext
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
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

        public DbSet<Payment> Payments { get; set; }
        public DbSet<MockPayement> MockPayments { get; set; }
    }

    public class MockPayement
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid PaymentReference { get; set; }
        public Guid ProviderReference { get; set; }
        public string PaymentDescription { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid PaymentReference { get; set; }
    }

    public enum PaymentStatus
    {
        New,
        Completed,
        Failed
    }
}
