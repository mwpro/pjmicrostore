using System.Threading.Tasks;
using Checkout.Payments.Contracts;
using Checkout.Shipping.Contracts.ApiModels;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Checkout.Orders.Services
{
    public interface IShippingService
    {
        Task<ShippingMethodDto> GetShippingMethod(string shippingMethodName);
    }

    public class ShippingService : IShippingService
    {
        private readonly IConfiguration _configuration;

        public ShippingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ShippingMethodDto> GetShippingMethod(string shippingMethodName)
        {
            var shippingMethodDto = await $"{_configuration.GetValue<string>("Dependencies:Shipping")}/api/"
                .AppendPathSegments("shipping", shippingMethodName)
                .GetJsonAsync<ShippingMethodDto>();

            return shippingMethodDto;
        }
    }
}