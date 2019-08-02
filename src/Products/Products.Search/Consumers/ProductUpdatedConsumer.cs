using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Nest;
using Products.Catalog.Contracts.Events;
using Products.Search.Controllers;

namespace Products.Search.Consumers
{
    public class ProductUpdatedConsumer : IConsumer<Batch<ProductUpdatedEvent>>
    {
        private ElasticClient _client;

        public ProductUpdatedConsumer(IConfiguration configurationRoot)
        {
            var node = new Uri($"{configurationRoot.GetValue<string>("ElasticSearch:Host")}:{configurationRoot.GetValue<string>("ElasticSearch:Port")}");
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

            var productsToUpdate = productsWithoutDuplicates.Where(x => x.Message.ProductDetails.IsActive && !x.Message.ProductDetails.IsDeleted)
                .Select(x => new ProductSearchModel(x.Message.ProductDetails)).ToList();
            var productsToDelete = productsWithoutDuplicates.Where(x => !x.Message.ProductDetails.IsActive || x.Message.ProductDetails.IsDeleted)
                .Select(x => new ProductSearchModel(x.Message.ProductDetails)).ToList();

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
        }
    }
}
