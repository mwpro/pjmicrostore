using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

            return Ok(new CartDto(cart));
        }

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

            return Ok(new CartDto(cart));
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

            return Ok(new CartDto(cart));
        }

        [HttpDelete("products/{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            var cart = GetOrCreateCart();
            
            cart.DeleteProduct(productId);
            _cartContext.SaveChanges();

            return Ok(new CartDto(cart));
        }

        private async Task<Product> GetProduct(int productId)
        {
            var product = _cartContext.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                product = await _productsService.GetProduct(productId);
            }

            return product;
        }

        private Domain.Cart GetOrCreateCart()
        {
            var cart = _cartContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == CartIdMock);

            if (cart == null)
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

    public class UpdateProductModel
    {
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
    }

    public class CartDto
    {
        public CartDto() { }

        public CartDto(Domain.Cart cart)
        {
            CartId = cart.Id;
            CartItems = cart.CartItems.Select(x => new CartItemDto(x)).ToList();
        }

        public int CartId { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public decimal Total => CartItems.Sum(x => x.Value);

        public class CartItemDto
        {
            public CartItemDto() { }

            public CartItemDto(CartItem cartItem)
            {
                ProductId = cartItem.ProductId;
                ProductName = cartItem.Product.Name;
                Quantity = cartItem.Quantity;
                ProductPrice = cartItem.Product.Price;
            }

            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Value => Quantity * ProductPrice;
        }
    }
}
