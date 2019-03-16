using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
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

        public CartController(CartContext cartContext)
        {
            _cartContext = cartContext;
        }

        [HttpGet("")]
        public IActionResult GetCart()
        {
            var cart = GetOrCreateCart();

            return Ok(new CartDto(cart));
        }

        [HttpPost("products/{productId}")]
        public IActionResult AddProduct(int productId, UpdateProductModel updateProductModel)
        {
            var cart = GetOrCreateCart();

            var product = GetProduct(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            cart.AddProduct(product, updateProductModel.Quantity);
            _cartContext.SaveChanges();

            return Ok(new CartDto(cart));
        }

        [HttpPut("products/{productId}")]
        public IActionResult UpdateProduct(int productId, UpdateProductModel updateProductModel)
        {
            var cart = GetOrCreateCart();

            var product = GetProduct(productId);
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

            var product = GetProduct(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            cart.DeleteProduct(productId);
            _cartContext.SaveChanges();

            return Ok(new CartDto(cart));
        }

        private Product GetProduct(int productId)
        {
            var product = _cartContext.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                product = new Product() // todo mock
                {
                    Id = productId,
                    Name = $"Product-{productId}",
                    Price = productId * 1.23m
                };
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
