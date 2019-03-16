﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Checkout.Orders.Domain;
using Checkout.Orders.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private const int CartIdMock = 1; // todo what next?
        private readonly ICartsService _cartsService;
        private readonly OrdersContext _ordersContext;

        public OrdersController(ICartsService cartsService, OrdersContext ordersContext)
        {
            _cartsService = cartsService;
            _ordersContext = ordersContext;
        }

        // TODO place order
        [HttpPost("")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            var cart = await _cartsService.GetCart(CartIdMock);

            // todo publish event
            var order = new Order()
            {
                CreateDate = DateTime.UtcNow,
                Customer = new Customer()
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jan@kowalski.pl",
                    Phone = "123-123-123"
                },
                Status = "New",
                OrderLines = cart.CartItems.Select(x => new OrderLine()
                {
                    Quantity = x.Quantity,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice
                }).ToList()
            };
            _ordersContext.Add(order);
            _ordersContext.SaveChanges();

            return StatusCode((int)HttpStatusCode.Created, order);
        }

        // TODO get order
    }

    public class PlaceOrderModel
    {

    }
}
