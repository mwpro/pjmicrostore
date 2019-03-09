using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Products.Catalog.Domain
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) }));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeValue>()
                .HasKey(x => new {x.ProductId, x.AttributeId});

            modelBuilder.Entity<AttributeValue>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Attributes)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<AttributeValue>()
                .HasOne(x => x.Attribute)
                .WithMany()
                .HasForeignKey(x => x.AttributeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<AttributeValue> Attributes { get; set; }
        // todo vat
        // todo promo
        // todo variants
        // todo photos

    }

    public class AttributeValue
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        public string Value { get; set; }
    }

    public class Attribute
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Child { get; set; }

        public ICollection<Product> Products { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryDto> Child { get; set; }

        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Child = category.Child.Select(CategoryDto.Map).ToList()
            };
        }
    }
}
