using System.Collections.Generic;
using System.Linq;

namespace Products.Search.Services
{
    public class ProductsList
    {
        public int ProductsCount { get; set; }
        public List<Product> Products { get; set; }
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

    }

    public class AttributeValue
    {
        public int AttributeId { get; set; }
        public string Name { get; set; }
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
}
