using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Domain;
using MediatR;

namespace Checkout.Orders.CommandHandlers
{
    public class MakeOrderWaitForShippingCommandHandler : IRequestHandler<MakeOrderWaitForShippingCommand>
    {
        private readonly OrdersContext _ordersContext;

        public MakeOrderWaitForShippingCommandHandler(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<Unit> Handle(MakeOrderWaitForShippingCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersContext.Order.FirstOrDefault(x => x.Id == request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found"); // todo custom exception type
            }

            order.MarkAsWaitingForShipping();

            await _ordersContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}