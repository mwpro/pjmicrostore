using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Products.Search.Services;

namespace Products.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ElasticClient _client;
        private readonly IProductsService _productsService;

        public SearchController(IProductsService productsService)
        {
            _productsService = productsService;
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultMappingFor(typeof(ProductSearchModel), idx => idx.IndexName("products"));
            settings.OnRequestCompleted(details =>
            {
                Console.WriteLine(details.Uri);
                var body = Encoding.UTF8.GetString(details.RequestBodyInBytes);
            });
            _client = new ElasticClient(settings);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var results = await _client.SearchAsync<ProductSearchModel>(q => q
                .RequestConfiguration(descriptor => descriptor.DisableDirectStreaming())
                .Size(10)
                .Aggregations(a => a.Nested(nameof(ProductSearchModel.StringAttributes), nested => nested
                    .Path(model => model.StringAttributes)
                    .Aggregations(descriptor => descriptor
                        .Terms(nameof(StringAttributeValue.AttributeName), nameTerms => nameTerms
                            .Field(model => model.StringAttributes.Suffix("attributeName"))
                            .Aggregations(containerDescriptor => containerDescriptor.Terms(nameof(StringAttributeValue.AttributeValue), valueTerms => valueTerms
                                .Field(model => model.StringAttributes.Suffix("attributeValue"))
                            )))
                        )    
                    )
                    )
                );
            var facets = results.Aggregations.Nested(nameof(ProductSearchModel.StringAttributes));
            var facetsTerms = facets.Terms(nameof(StringAttributeValue.AttributeName));
            var stringAttributes = facetsTerms.Buckets.Select(x => new SearchStringAttributeModel()
            {
                AttributeName = x.Key,
                Sum = x.DocCount ?? 0,
                StringValueAttributeValues = x.Terms(nameof(StringAttributeValue.AttributeValue))
                    .Buckets.Select(v => new SearchStringAttributeModel.SearchStringValueAttributeModel()
                    {
                        Value = v.Key,
                        Count = v.DocCount ?? 0
                    })
            }).ToList();
            
            return Ok(new { documents = results.Documents, stringAttributes = stringAttributes});
        }

        [HttpGet("import")]
        public async Task<IActionResult> Import()
        {
            var products = await _productsService.GetProducts();

            var searchModels = products.Select(x => new ProductSearchModel(x)).ToList();

            var response = await _client.IndexManyAsync(searchModels, IndexName.From<ProductSearchModel>());

            return Accepted(response);
        }
    }

    public class SearchStringAttributeModel
    {
        public string AttributeName { get; set; }
        public long Sum { get; set; }

        public IEnumerable<SearchStringValueAttributeModel> StringValueAttributeValues { get; set; }

        public class SearchStringValueAttributeModel
        {
            public string Value { get; set; }
            public long Count { get; set; }
        }
    }

    public class ProductSearchModel
    {
        public ProductSearchModel()
        {

        }

        public ProductSearchModel(Product product)
        {
            Id = product.Id;

            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

            IsActive = product.IsActive;
            IsDeleted = product.IsDeleted;

            StringAttributes = product.Attributes.Select(y => new StringAttributeValue(y)).ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<StringAttributeValue> StringAttributes { get; set; }
    }

    public class StringAttributeValue
    {
        public StringAttributeValue()
        {

        }

        public StringAttributeValue(AttributeValue attributeValue)
        {
            AttributeName = attributeValue.Name;
            AttributeValue = attributeValue.Value;
        }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
