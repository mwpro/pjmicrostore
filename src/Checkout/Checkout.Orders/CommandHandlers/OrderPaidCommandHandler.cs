using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Domain;
using MediatR;

namespace Checkout.Orders.CommandHandlers
{
    public class OrderPaidCommandHandler : IRequestHandler<OrderPaidCommand>
    {
        private readonly OrdersContext _ordersContext;

        public OrderPaidCommandHandler(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<Unit> Handle(OrderPaidCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersContext.Order.FirstOrDefault(x => x.Id == request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found"); // todo custom exception type
            }

            order.MarkAsPaid();

            await _ordersContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}