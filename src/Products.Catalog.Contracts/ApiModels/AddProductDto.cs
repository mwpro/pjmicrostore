using System.Collections.Generic;

namespace Products.Catalog.Contracts.ApiModels
{

    public class AddProductDto
    {
        public AddProductDto()
        {
            Attributes = new List<AddAttributeDto>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<AddAttributeDto> Attributes { get; set; }

    }
}
