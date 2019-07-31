using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Catalog.Contracts.ApiModels;
using Products.Catalog.Contracts.Events;
using Products.Catalog.Domain;

namespace Products.Catalog.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _productsContext;
        private readonly IBus _bus;

        public ProductsController(ProductsContext productsContext, IBus bus)
        {
            _productsContext = productsContext;
            _bus = bus;
        }
        
        [HttpGet("")]
        [Authorize(AuthorizationPolicies.AdminOnly)]
        public IActionResult GetAllProducts([FromQuery]int page = 1,
            int productsPerPage = 10)
        {
            if (page < 1 || productsPerPage < 1)
                return BadRequest("Invalid pagination parameters");
            
            var products = _productsContext.Products
                .Include(x => x.Category)
                .Include(x => x.Attributes)
                .ThenInclude(x => x.Attribute)
                .Skip(productsPerPage * (page - 1))
                .Take(productsPerPage)
                .AsNoTracking()
                .Select(product => ToProductDto(product)).ToList();

            var productIds = products.Select(x => x.Id).ToList();

            var productsCount = _productsContext.Products.Count();
            
            return Ok(new ProductsList()
            {
                Products = products,
                ProductsCount = productsCount
            });
        }

        [HttpPost("")]
        [Authorize(AuthorizationPolicies.AdminOnly)]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            // todo validation
            var category = _productsContext.Categories.FirstOrDefault(x => x.Id == addProductDto.CategoryId);
            if (category == null)
                return BadRequest("Category not found");

            var attributeIds = addProductDto.Attributes.Select(y => y.AttributeId).ToList();
            var attributes = _productsContext.Attributes
                .Where(x => attributeIds.Contains(x.Id)).ToList();
            var notFoundAttributes = addProductDto.Attributes.Where(x => attributes.All(y => y.Id != x.AttributeId));
            if (notFoundAttributes.Any())
                return BadRequest("Attributes not found");
            
            var product = new Product()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                CategoryId = addProductDto.CategoryId,
                Category = category,
                IsActive = addProductDto.IsActive,
                Attributes = addProductDto.Attributes.Select(x => new AttributeValue()
                {
                    AttributeId = x.AttributeId,
                    Value = x.AttributeValue,
                    Attribute = attributes.First(y => y.Id == x.AttributeId)
                }).ToList()
            };

            _productsContext.Products.Add(product);
            await _productsContext.SaveChangesAsync();

            var productDto = ToProductDto(product);
            await _bus.Publish(new ProductUpdatedEvent()
            {
                ProductDetails = productDto,
                UpdateDateUtc = DateTime.UtcNow
            });

            return Created($"api/products/{product.Id}", productDto); // todo references not initialized
        }

        [HttpPut("{productId}")]
        [Authorize(AuthorizationPolicies.AdminOnly)]
        public async Task<IActionResult> EditProduct(int productId, AddProductDto addProductDto)
        {
            var product = _productsContext.Products
                .Include(x => x.Attributes)
                .FirstOrDefault(x => x.Id == productId);
            // todo validation
            if (product == null)
                return NotFound();

            var category = _productsContext.Categories.FirstOrDefault(x => x.Id == addProductDto.CategoryId);
            if (category == null)
                return BadRequest("Category not found");

            var attributeIds = addProductDto.Attributes.Select(y => y.AttributeId).ToList();
            var attributes = _productsContext.Attributes
                .Where(x => attributeIds.Contains(x.Id)).ToList();
            var notFoundAttributes = addProductDto.Attributes.Where(x => attributes.All(y => y.Id != x.AttributeId));
            if (notFoundAttributes.Any())
                return BadRequest("Attributes not found");

            product.Name = addProductDto.Name;
            product.Description = addProductDto.Description;
            product.Price = addProductDto.Price;
            product.CategoryId = addProductDto.CategoryId;
            product.Category = category;
            product.IsActive = addProductDto.IsActive;

            product.Attributes = addProductDto.Attributes.Select(x => new AttributeValue()
            {
                AttributeId = x.AttributeId,
                Value = x.AttributeValue,
                Attribute = attributes.First(y => y.Id == x.AttributeId)
            }).ToList();

            await _productsContext.SaveChangesAsync();

            var productDto = ToProductDto(product);

            await _bus.Publish(new ProductUpdatedEvent()
            {
                ProductDetails = productDto,
                UpdateDateUtc = DateTime.UtcNow
            });
            
            return Created($"api/products/{product.Id}", productDto); // todo references not initialized
        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
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
            
            var result = ToProductDto(product);

            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpGet("/api/categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _productsContext.Categories
                .Include(x => x.Child).AsNoTracking().ToList();

            return Ok(categories.Where(x => x.Parent == null)
                .Select(CategoryTreeDto.Map));
        }

        [Authorize(AuthorizationPolicies.AdminOnly)]
        [HttpGet("/api/attributes")]
        public IActionResult GetAllAttributes()
        {
            var attributes = _productsContext.Attributes
                .AsNoTracking().ToList();

            return Ok(attributes);
        }

        private static ProductDto ToProductDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = new CategoryDto
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                },
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                Attributes = product.Attributes.Select(x => new AttributeValueDto()
                {
                    AttributeId = x.AttributeId,
                    Name = x.Attribute.Name,
                    Value = x.Value
                }).ToList(),
            };
        }
    }
}
