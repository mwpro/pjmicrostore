using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Products.Catalog.Domain
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }

        // todo public bool IsActive { get; set; }
        // todo public bool IsDeleted { get; set; }
        // todo attributes
        // todo vat
        // todo promo
        // todo variants
        // todo photos

    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

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
