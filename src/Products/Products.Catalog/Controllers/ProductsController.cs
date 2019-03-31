using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Catalog.Domain;

namespace Products.Catalog.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _productsContext;

        public ProductsController(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }
        
        [HttpGet("products")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productsContext.Products
                .Include(x => x.Category)
                .Include(x => x.Attributes)
                    .ThenInclude(x => x.Attribute)
                .AsNoTracking()
                .Select(x => new ProductDto(x)).ToList());
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            // todo validation

            var product = new Product()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                CategoryId = addProductDto.CategoryId,
                Attributes = addProductDto.Attributes.Select(x => new AttributeValue()
                {
                    AttributeId = x.AttributeId,
                    Value = x.AttributeValue
                }).ToList()
            };

            _productsContext.Products.Add(product);
            await _productsContext.SaveChangesAsync();

            return Created($"api/products/{product.Id}", product); // todo references not initialized
        }

        [HttpGet("products/{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var product = _productsContext.Products
                .Include(x => x.Category)
                .Include(x => x.Attributes)
                .ThenInclude(x => x.Attribute)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == productId);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _productsContext.Categories
                .Include(x => x.Child).AsNoTracking().ToList();

            return Ok(categories.Where(x => x.Parent == null)
                .Select(CategoryTreeDto.Map));
        }


        [HttpGet("attributes")]
        public IActionResult GetAllAttributes()
        {
            var attributes = _productsContext.Attributes
                .AsNoTracking().ToList();

            return Ok(attributes);
        }
    }


    public class AddProductDto
    {
        public AddProductDto()
        {
            Attributes = new List<AddAttributeDto>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<AddAttributeDto> Attributes { get; set; }

    }

    public class AddAttributeDto
    {
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }

    public class ProductDto
    {
        public ProductDto(Product product)
        {
            Id = product.Id;

            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

            CategoryId = product.CategoryId;
            Category = new CategoryDto(product.Category);

            IsActive = product.IsActive;
            IsDeleted = product.IsDeleted;

            Attributes = product.Attributes.Select(x => new AttributeValueDto(x)).ToList();
        }

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

    public class AttributeValueDto
    {
        public AttributeValueDto(AttributeValue attributeValue)
        {
            AttributeId = attributeValue.AttributeId;
            Name = attributeValue.Attribute.Name;

            Value = attributeValue.Value;
        }

        public int AttributeId { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class CategoryDto
    {
        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
