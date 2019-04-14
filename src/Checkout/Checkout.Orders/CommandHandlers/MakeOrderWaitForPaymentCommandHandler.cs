using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Domain;
using MediatR;

namespace Checkout.Orders.CommandHandlers
{
    public class MakeOrderWaitForPaymentCommandHandler : IRequestHandler<MakeOrderWaitForPaymentCommand>
    {
        private readonly OrdersContext _ordersContext;

        public MakeOrderWaitForPaymentCommandHandler(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<Unit> Handle(MakeOrderWaitForPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersContext.Order.FirstOrDefault(x => x.Id == request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found"); // todo custom exception type
            }

            order.MarkAsWaitingForPayment();

            await _ordersContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}