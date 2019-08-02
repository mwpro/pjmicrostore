using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Products.Catalog.Contracts.ApiModels;

namespace Products.Search.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }

    public class ProductsService : IProductsService
    {
        private readonly IConfiguration _configuration;

        public ProductsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var product = await $"{_configuration.GetValue<string>("Dependencies:Products")}/api/" // todo products or catalog?
                .AppendPathSegments("products")
                .SetQueryParam("productsPerPage", 1000) // todo
                .GetJsonAsync<ProductsList>();

            return product.Products;
        }
    }
}
