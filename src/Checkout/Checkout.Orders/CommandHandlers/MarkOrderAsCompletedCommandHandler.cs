using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Contracts.Events;
using Checkout.Orders.Domain;
using MassTransit;
using MediatR;

namespace Checkout.Orders.CommandHandlers
{
    public class MarkOrderAsCompletedCommandHandler : IRequestHandler<MarkOrderAsCompletedCommand>
    {
        private readonly OrdersContext _ordersContext;
        private readonly IBus _bus;

        public MarkOrderAsCompletedCommandHandler(OrdersContext ordersContext, IBus bus)
        {
            _ordersContext = ordersContext;
            _bus = bus;
        }

        public async Task<Unit> Handle(MarkOrderAsCompletedCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersContext.Order.FirstOrDefault(x => x.Id == request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found"); // todo custom exception type
            }

            order.MarkAsCompleted();

            await _ordersContext.SaveChangesAsync(cancellationToken);

            await _bus.Publish(new OrderCompletedEvent()
            {
                OrderId = order.Id
            }, cancellationToken);

            return Unit.Value;
        }
    }
}