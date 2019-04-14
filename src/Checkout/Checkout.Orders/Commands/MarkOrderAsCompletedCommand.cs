using MediatR;

namespace Checkout.Orders.Commands
{
    public class MarkOrderAsCompletedCommand : IRequest
    {
        public int OrderId { get; }

        public MarkOrderAsCompletedCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}