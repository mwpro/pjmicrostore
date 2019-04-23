using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
using MassTransit;
using Products.Catalog.Contracts.Events;

namespace Checkout.Cart.Consumers
{
    public class ProductUpdatedConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly CartContext _cartContext;

        public ProductUpdatedConsumer(CartContext cartContext)
        {
            _cartContext = cartContext;
        }
        
        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var product = _cartContext.Products.FirstOrDefault(x => x.Id == context.Message.ProductDetails.Id);
            if (product == null) // no product to update
                return;

            if (context.Message.ProductDetails.IsActive && !context.Message.ProductDetails.IsDeleted)
            {
                product.Name = context.Message.ProductDetails.Name;
                product.Price = context.Message.ProductDetails.Price;
            }
            else
            {
                _cartContext.Products.Remove(product);
            }

            
            await _cartContext.SaveChangesAsync();
        }
    }
}