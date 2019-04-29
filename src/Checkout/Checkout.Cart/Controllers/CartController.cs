using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Checkout.Cart.Domain;
using Checkout.Cart.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Cart.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private const int CartIdMock = 1; // todo what next?
        private readonly CartContext _cartContext;
        private readonly IProductsService _productsService;

        public CartController(CartContext cartContext, IProductsService productsService)
        {
            _cartContext = cartContext;
            _productsService = productsService;
        }

        [HttpGet("")]
        public IActionResult GetCart()
        {
            var cart = GetOrCreateCart();

            return Ok(MapToApiModel(cart));
        }


        [HttpGet("{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            var cart = _cartContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == cartId);

            if (cart == null)
                return NotFound();

            return Ok(MapToApiModel(cart));
        }

        [Authorize]
        [HttpPost("products/{productId}")]
        public async Task<IActionResult> AddProduct(int productId, UpdateProductModel updateProductModel)
        {
            var cart = GetOrCreateCart();

            var product = await GetProduct(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            

            cart.AddProduct(product, updateProductModel.Quantity);
            _cartContext.SaveChanges();

            return Ok(MapToApiModel(cart));
        }

        [HttpPut("products/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, UpdateProductModel updateProductModel)
        {
            var cart = GetOrCreateCart();

            var product = await GetProduct(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            cart.UpdateProduct(product, updateProductModel.Quantity);
            _cartContext.SaveChanges();

            return Ok(MapToApiModel(cart));
        }

        [HttpDelete("products/{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            var cart = GetOrCreateCart();
            
            cart.DeleteProduct(productId);
            _cartContext.SaveChanges();

            return Ok(MapToApiModel(cart));
        }

        private CartDto MapToApiModel(Domain.Cart cart)
        {
            return new CartDto()
            {
                CartId = cart.Id,
                Total = cart.Total,
                NumberOfItems = cart.NumberOfItems,
                CartItems = cart.CartItems.Select(x => 
                    new CartDto.CartItemDto()
                    {
                        Quantity = x.Quantity,
                        ProductPrice = x.Product.Price,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Value = x.Value
                    })
            };
        }

        private async Task<Product> GetProduct(int productId)
        {
            var product = _cartContext.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                var productDto = await _productsService.GetProduct(productId);
                if (!productDto.IsActive || productDto.IsDeleted)
                    return null;
                product = new Product()
                {
                    Id = productDto.Id,
                    Price = productDto.Price,
                    Name = productDto.Name
                };
            }

            return product;
        }

        private Domain.Cart GetOrCreateCart()
        {
            IQueryable<Domain.Cart> cartQuery = _cartContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product);

            if (User != null && Guid.TryParse(User.Identity.GetUserId(), out var userGuid))
            {
                cartQuery = cartQuery.Where(x => x.OwnerUserId == userGuid);
            }

            var cart = cartQuery.FirstOrDefault(x => x.Id == CartIdMock);

            9if (cart == null)
            {
                cart = new Domain.Cart()
                {
                    Id = CartIdMock
                };
                _cartContext.Add(cart);
            }

            return cart;
        }
    }

    public static class IdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            if (identity is ClaimsIdentity claimsIdentity)
            {
                var subIdentity = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "sub");
                return subIdentity?.Value;

            }
            return null;
        }
    }
}
