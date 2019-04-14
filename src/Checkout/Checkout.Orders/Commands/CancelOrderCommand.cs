using MediatR;

namespace Checkout.Orders.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public int OrderId { get; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}