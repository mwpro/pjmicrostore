using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Products.Catalog.Contracts;
using Products.Catalog.Contracts.ApiModels;

namespace Checkout.Cart.Services
{
    public interface IProductsService
    {
        Task<ProductDto> GetProduct(int id);
    }

    public class ProductsService : IProductsService
    {
        private readonly IConfiguration _configuration;

        public ProductsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await $"{_configuration.GetValue<string>("Dependencies:Products")}/api/"
                .AppendPathSegments("products", id)
                .GetJsonAsync<ProductDto>();

            return product;
        }
    }
}
