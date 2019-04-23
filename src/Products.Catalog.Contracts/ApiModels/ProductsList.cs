using System.Collections.Generic;

namespace Products.Catalog.Contracts.ApiModels
{
    public class ProductsList
    {
        public int ProductsCount { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}