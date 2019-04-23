using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nest;
using Products.Catalog.Contracts.Events;
using Products.Search.Controllers;

namespace Products.Search.Consumers
{
    public class ProductUpdatedConsumer : IConsumer<Batch<ProductUpdatedEvent>>
    {
        private ElasticClient _client;

        public ProductUpdatedConsumer()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultMappingFor(typeof(ProductSearchModel), idx => idx.IndexName("products"));
            settings.ThrowExceptions(true);

            _client = new ElasticClient(settings);
        }

        public async Task Consume(ConsumeContext<Batch<ProductUpdatedEvent>> context)
        {
            var productsWithoutDuplicates = context.Message.GroupBy(x => x.Message.ProductDetails.Id)
                .Select(x => x.OrderByDescending(y => y.Message.UpdateDateUtc).FirstOrDefault())
                .ToList();
            
            var searchModels = productsWithoutDuplicates.Select(x => new ProductSearchModel(x.Message.ProductDetails)).ToList();

            var response = await _client.IndexManyAsync(searchModels, IndexName.From<ProductSearchModel>());
            if (response.OriginalException != null)
                throw response.OriginalException;
        }
    }
}
