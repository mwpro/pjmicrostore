using System.Collections.Generic;

namespace Products.Catalog.Contracts.ApiModels
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<AttributeValueDto> Attributes { get; set; }
    }
}