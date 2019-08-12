using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Infrastructure;
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
        private readonly IAuthorizationTokenService _authorizationTokenService;

        public ProductsService(IConfiguration configuration, IAuthorizationTokenService authorizationTokenService)
        {
            _configuration = configuration;
            _authorizationTokenService = authorizationTokenService;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var product = await $"{_configuration.GetValue<string>("Dependencies:Products")}/api/"
                .WithOAuthBearerToken(await _authorizationTokenService.GetBearerToken())
                .AppendPathSegments("products")
                .SetQueryParam("productsPerPage", 1000) // todo
                .GetJsonAsync<ProductsList>();

            return product.Products;
        }
    }
}
