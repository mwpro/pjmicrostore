using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Products.Catalog.Contracts;
using Products.Catalog.Contracts.ApiModels;

namespace Products.Search.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }

    public class ProductsService : IProductsService
    {
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var product = await "http://localhost:53606/api/"
                .AppendPathSegments("products")
                .SetQueryParam("productsPerPage", 1000) // todo
                .GetJsonAsync<ProductsList>();

            return product.Products;
        }
    }
}
