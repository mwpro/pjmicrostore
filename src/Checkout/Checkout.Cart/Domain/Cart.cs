using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Checkout.Cart.Domain
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
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

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }

    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public void AddProduct(Product product, int quantity)
        {
            var existingCartItem = CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (existingCartItem != null)
            {
                UpdateProduct(existingCartItem.Product, existingCartItem.Quantity + quantity);
            }
            else
            {
                CartItems.Add(new CartItem()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void UpdateProduct(Product product, int quantity)
        {
            var existingCartItem = CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (existingCartItem == null)
            {
                AddProduct(product,  quantity);
            }
            else
            {
                existingCartItem.Quantity = quantity;
            }
        }

        public void DeleteProduct(int productId)
        {
            var existingCartItem = CartItems.FirstOrDefault(x => x.ProductId == productId);
            CartItems.Remove(existingCartItem);
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }

    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
