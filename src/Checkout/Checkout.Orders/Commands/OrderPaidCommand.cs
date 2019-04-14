using MediatR;

namespace Checkout.Orders.Commands
{
    public class OrderPaidCommand : IRequest
    {
        public int OrderId { get; }

        public OrderPaidCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}