using MediatR;

namespace Checkout.Orders.Commands
{
    public class MakeOrderWaitForPaymentCommand : IRequest
    {
        public int OrderId { get; }

        public MakeOrderWaitForPaymentCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}