using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
using Flurl;
using Flurl.Http;

namespace Checkout.Cart.Services
{
    public interface IProductsService
    {
        Task<Product> GetProduct(int id);
    }

    public class ProductsService : IProductsService
    {
        public async Task<Product> GetProduct(int id)
        {
            var product = await "http://localhost:53606/api/"
                .AppendPathSegments("products", id)
                .GetJsonAsync<Product>();

            return product;
        }
    }
}
