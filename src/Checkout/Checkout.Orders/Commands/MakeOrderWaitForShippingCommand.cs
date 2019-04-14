using MediatR;

namespace Checkout.Orders.Commands
{
    public class MakeOrderWaitForShippingCommand : IRequest
    {
        public int OrderId { get; }

        public MakeOrderWaitForShippingCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}