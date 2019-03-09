using System;
using System.Collections.Generic;
using System.Linq;
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
                .AsNoTracking().ToList());
        }

        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _productsContext.Categories
                .Include(x => x.Child).AsNoTracking().ToList();

            return Ok(categories.Where(x => x.Parent == null)
                .Select(CategoryDto.Map));
        }
    }
}
