using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Nest;
using Products.Photos.Contracts.Events;
using Products.Search.Controllers;

namespace Products.Search.Consumers
{
    public class PhotoUpdatesConsumer : IConsumer<PhotoAddedEvent>,
        IConsumer<PhotoRemovedEvent>
    {
        private ElasticClient _client;

        public PhotoUpdatesConsumer(IConfiguration configurationRoot)
        {
            var node = new Uri($"{configurationRoot.GetValue<string>("ElasticSearch:Host")}:{configurationRoot.GetValue<string>("ElasticSearch:Port")}");
            var settings = new ConnectionSettings(node);
            settings.DefaultMappingFor(typeof(ProductSearchModel), idx => idx.IndexName("products"));
            settings.DefaultMappingFor(typeof(PhotoSearchModel), idx => idx.IndexName("photos"));
            settings.ThrowExceptions(true);

            _client = new ElasticClient(settings);
        }

        public async Task Consume(ConsumeContext<PhotoAddedEvent> context)
        {
            await _client.IndexDocumentAsync(new PhotoSearchModel()
            {
                OriginalUrl = context.Message.OriginalUrl,
                PhotoId = context.Message.PhotoId,
                ProductId = context.Message.ProductId
            });
        }

        public async Task Consume(ConsumeContext<PhotoRemovedEvent> context)
        {
            await _client.DeleteAsync(new DeleteRequest(IndexName.From<PhotoSearchModel>(), TypeName.From<PhotoSearchModel>(), Id.From(new PhotoSearchModel()
            {
                OriginalUrl = context.Message.OriginalUrl,
                PhotoId = context.Message.PhotoId,
                ProductId = context.Message.ProductId
            })));
        }
    }
}