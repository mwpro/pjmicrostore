using System.Linq;
using Checkout.Shipping.Contracts.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Shipping.Controllers
{
    [Route("api/shipping")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private static readonly ShippingMethodDto[] ShippingMethods = new[]
        {
            new ShippingMethodDto()
            {
                Name = "StorePickup",
                Price = 0.00m
            },
            new ShippingMethodDto()
            {
                Name = "Courier",
                Price = 20.00m
            },
            new ShippingMethodDto()
            {
                Name = "Post",
                Price = 10.00m
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ShippingMethods);
        }


        [HttpGet("{shippingMethodName}")]
        public IActionResult GetMethod(string shippingMethodName)
        {
            var shippingMethod = ShippingMethods.FirstOrDefault(x => x.Name == shippingMethodName);
            if (shippingMethod == null)
                return NotFound();

            return Ok(shippingMethod);
        }
    }
}
