using System;
using MediatR;

namespace Checkout.Orders.Commands
{
    public class PlaceOrderCommand : IRequest<PlaceOrderCommandResponse>
    {
        public int CartId { get; }
        public string PaymentMethod { get; } // todo method id or code?

        public PlaceOrderCommand(int cartId, string paymentMethod)
        {
            CartId = cartId;
            PaymentMethod = paymentMethod;
        }
    }

    public class PlaceOrderCommandResponse : IRequest
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public Guid? PaymentReference { get; set; }
    }
}
