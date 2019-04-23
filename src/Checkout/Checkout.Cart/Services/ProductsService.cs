using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
using Flurl;
using Flurl.Http;
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
        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await "http://localhost:53606/api/"
                .AppendPathSegments("products", id)
                .GetJsonAsync<ProductDto>();

            return product;
        }
    }
}
