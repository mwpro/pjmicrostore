using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using Products.Catalog.Contracts;
using Products.Catalog.Contracts.ApiModels;
using Products.Search.Services;

namespace Products.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ElasticClient _client;
        private readonly IProductsService _productsService;

        public SearchController(IProductsService productsService, IConfiguration configurationRoot)
        {
            _productsService = productsService;
            var node = new Uri($"{configurationRoot.GetValue<string>("ElasticSearch:Host")}:{configurationRoot.GetValue<string>("ElasticSearch:Port")}");
            var settings = new ConnectionSettings(node);
            settings.DefaultMappingFor(typeof(ProductSearchModel), idx => idx.IndexName("products"));
            settings.DefaultMappingFor(typeof(PhotoSearchModel), idx => idx.IndexName("photos"));
            settings.ThrowExceptions(true);

            _client = new ElasticClient(settings);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]SearchModel searchModel)
        {
            var results = await _client.SearchAsync<ProductSearchModel>(q =>
            {
                q = q.Query(query => query
                    .Bool(boolQuery =>
                    {
                        var filters = new List<Func<QueryContainerDescriptor<ProductSearchModel>, QueryContainer>>();
                        if (searchModel.StringAttr?.Any() ?? false)
                        {
                            var stringAttrFilters = searchModel.StringAttr
                            .Select<KeyValuePair<string, string[]>,
                                Func<QueryContainerDescriptor<ProductSearchModel>, QueryContainer>>(
                                attribute =>
                                    outerFilter => outerFilter
                                        .Bool(innerBool => innerBool
                                            .Should(
                                                attribute.Value
                                                    .Select<string,
                                                        Func<QueryContainerDescriptor<ProductSearchModel>,
                                                            QueryContainer>>(attributeValue =>
                                                        should => should.Nested(nested =>
                                                            nested
                                                                .Path("stringAttributes")
                                                                .Query(innerQuery => innerQuery
                                                                    .Bool(attributeValueBool =>
                                                                        attributeValueBool
                                                                            .Filter(new TermQuery()
                                                                            {
                                                                                Field =
                                                                                    "stringAttributes.attributeName",
                                                                                Value = attribute.Key
                                                                            }, new TermQuery()
                                                                            {
                                                                                Field =
                                                                                    "stringAttributes.attributeValue",
                                                                                Value = attributeValue
                                                                            })))
                                                        ))
                                            )));
                            filters.AddRange(stringAttrFilters);
                        }

                        if (searchModel.CategoryId.HasValue)
                        {
                            filters.Add(descriptor => descriptor.Term(model =>
                                model.Field(productSearchModel => productSearchModel.CategoryId)
                                    .Value(searchModel.CategoryId)));
                        }

                        return boolQuery.Filter(filters);
                    }));

                return q
                    .Size(50) // todo pagination
                    .RequestConfiguration(descriptor => descriptor.DisableDirectStreaming())
                    .Aggregations(a => a.Nested(nameof(ProductSearchModel.StringAttributes), nested => nested
                            .Path(model => model.StringAttributes)
                            .Aggregations(descriptor => descriptor
                                .Terms(nameof(StringAttributeValue.AttributeName), nameTerms => nameTerms
                                    .Field(model => model.StringAttributes.Suffix("attributeName"))
                                    .Aggregations(containerDescriptor => containerDescriptor.Terms(
                                        nameof(StringAttributeValue.AttributeValue), valueTerms => valueTerms
                                            .Field(model => model.StringAttributes.Suffix("attributeValue"))
                                    )))
                            )
                        )
                    );
            });
            
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

            var photos = await _client.SearchAsync<PhotoSearchModel>(descriptor => descriptor.Query(q =>
                q.Bool(b => b.Filter(bq => bq.Terms(t => t.Field(o => o.ProductId)
                    .Terms(results.Documents.Select(x => x.Id)))))
            ));

            foreach (var product in results.Documents)
            {
                var productPhotos = photos.Documents.Where(x => x.ProductId == product.Id);
                if (productPhotos.Any())
                {
                    product.SearchThumbnailUri = productPhotos.FirstOrDefault().OriginalUrl;
                }
            }

            return Ok(new
            {
                documents = results.Documents,
                stringAttributes = stringAttributes,
                query = Encoding.UTF8.GetString(results.ApiCall.RequestBodyInBytes)
            });
        }

        [HttpGet("import")] // todo this endpoint is test only...
        public async Task<IActionResult> Import()
        {
            var products = await _productsService.GetProducts();

            var productsToUpdate = products.Where(x => x.IsActive && !x.IsDeleted)
                .Select(x => new ProductSearchModel(x)).ToList();
            var productsToDelete = products.Where(x => !x.IsActive || x.IsDeleted)
                .Select(x => new ProductSearchModel(x)).ToList();

            if (productsToUpdate.Any())
            {
                var updateResponse = await _client.IndexManyAsync(productsToUpdate, IndexName.From<ProductSearchModel>());
                if (updateResponse.OriginalException != null)
                    throw updateResponse.OriginalException;
            }

            if (productsToDelete.Any())
            {
                var deleteResponse = await _client.DeleteManyAsync(productsToDelete, IndexName.From<ProductSearchModel>());
                if (deleteResponse.OriginalException != null)
                    throw deleteResponse.OriginalException;
            }

            return Accepted();
        }
    }

    public class SearchModel
    {
        public Dictionary<string, string[]> StringAttr { get; set; }
        public int? CategoryId { get; set; }
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

    public class PhotoSearchModel
    {
        public int ProductId { get; set; }
        public int PhotoId { get; set; }

        public string OriginalUrl { get; set; }
    }

    public class ProductSearchModel
    {
        public ProductSearchModel()
        {

        }

        public ProductSearchModel(ProductDto product)
        {
            Id = product.Id;

            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

            CategoryId = product.CategoryId;
            
            StringAttributes = product.Attributes.Select(y => new StringAttributeValue(y)).ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string SearchThumbnailUri { get; set; } // todo populate somehow

        public IEnumerable<StringAttributeValue> StringAttributes { get; set; }
    }
    
    public class StringAttributeValue
    {
        public StringAttributeValue()
        {

        }

        public StringAttributeValue(AttributeValueDto attributeValue)
        {
            AttributeName = attributeValue.Name;
            AttributeValue = attributeValue.Value;
        }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
