using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Checkout.Cart.Domain;
using Checkout.Cart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Cart.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartContext _cartContext;
        private readonly IProductsService _productsService;

        public CartController(CartContext cartContext, IProductsService productsService)
        {
            _cartContext = cartContext;
            _productsService = productsService;
        }

        [HttpGet("")]
        public IActionResult GetCart(Guid? cartToken)
        {
            var cart = GetOrCreateCart(cartToken);

            return Ok(MapToApiModel(cart));
        }


        [HttpGet("{cartId}")]
        public IActionResult GetCart(Guid cartId)
        {
            var cart = _cartContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.CartAccessToken == cartId);

            if (cart == null)
                return NotFound();

            return Ok(MapToApiModel(cart));
        }

        [HttpPost("products/{productId}")]
        public async Task<IActionResult> AddProduct(int productId, UpdateProductModel updateProductModel, Guid? cartToken)
        {
            var cart = GetOrCreateCart(cartToken);

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
        public async Task<IActionResult> UpdateProduct(int productId, UpdateProductModel updateProductModel, Guid? cartToken)
        {
            var cart = GetOrCreateCart(cartToken);

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
        public IActionResult DeleteProduct(int productId, Guid? cartToken)
        {
            var cart = GetOrCreateCart(cartToken);
            
            cart.DeleteProduct(productId);
            _cartContext.SaveChanges();

            return Ok(MapToApiModel(cart));
        }

        private CartDto MapToApiModel(Domain.Cart cart)
        {
            return new CartDto()
            {
                CartId = cart.Id,
                CartAccessToken = cart.CartAccessToken,
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

        private Domain.Cart GetOrCreateCart(Guid? cartToken)
        {
            if (!cartToken.HasValue)
            {
                cartToken = Guid.NewGuid();
            }

            var cart = _cartContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => 
                    //x.Id == CartIdMock
                    x.CartAccessToken == cartToken
                );
            
            if (cart == null)
            {
                cart = new Domain.Cart()
                {
                    CartAccessToken = cartToken
                };
                _cartContext.Add(cart);
                _cartContext.SaveChanges(); // todo not good
            }

            return cart;
        }
    }
}
