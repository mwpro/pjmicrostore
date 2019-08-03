using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Shipping.Controllers
{
    [Route("api/shipping")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private static PaymentMethodDto[] PaymentMethods = new[]
        {
            new PaymentMethodDto()
            {
                Name = "StorePickup",
                Price = 0.00m
            },
            new PaymentMethodDto()
            {
                Name = "Courier",
                Price = 20.00m
            },
            new PaymentMethodDto()
            {
                Name = "Post",
                Price = 10.00m
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(PaymentMethods);
        }
    }

    public class PaymentMethodDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
         
    }
}
