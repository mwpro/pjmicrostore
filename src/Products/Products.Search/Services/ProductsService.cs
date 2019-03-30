using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Products.Search.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();
    }

    public class ProductsService : IProductsService
    {
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var product = await "http://localhost:53606/api/"
                .AppendPathSegments("products")
                .GetJsonAsync<IEnumerable<Product>>();

            return product;
        }
    }
}
