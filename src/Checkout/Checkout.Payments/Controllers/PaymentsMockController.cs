using System;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Payments.Contracts.Events;
using Checkout.Payments.Domain;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payments.Controllers
{
    [AllowAnonymous]
    [Route("api/payments/mock/")]
    [ApiController]
    public class PaymentsMockController : ControllerBase
    {
        private readonly PaymentsDbContext _paymentsDbContext;
        private readonly IBus _bus;

        public PaymentsMockController(PaymentsDbContext paymentsDbContext, IBus bus)
        {
            _paymentsDbContext = paymentsDbContext;
            _bus = bus;
        }

        // GET api/values/5
        [HttpGet("{providerReference}")]
        public ActionResult<string> Get(Guid providerReference)
        {
            var paymentMock = _paymentsDbContext.MockPayments.FirstOrDefault(x => x.ProviderReference == providerReference);
            if (paymentMock == null)
            {
                return NotFound();
            }

            return Ok(paymentMock); // todo model?

        }


        // GET api/values/5
        [HttpPost("{providerReference}/{success}")]
        public async Task<ActionResult<string>> Get(Guid providerReference, bool success)
        {
            var paymentMock = _paymentsDbContext.MockPayments.FirstOrDefault(x => x.ProviderReference == providerReference);
            if (paymentMock == null)
            {
                return NotFound();
            }

            if (paymentMock.PaymentStatus != PaymentStatus.New)
            {
                return BadRequest();
            }

            if (success)
            {
                await _bus.Publish(new PaymentMockPaid()
                {
                    PaymentReference = paymentMock.PaymentReference,
                    PaymentMockId = paymentMock.Id
                });
                paymentMock.PaymentStatus = PaymentStatus.Completed;
            }
            else
            {
                paymentMock.PaymentStatus = PaymentStatus.Failed;
            }

            _paymentsDbContext.SaveChanges();

            return Ok(new { returnUrl = "/orderPlaced" }); // todo thats mock of mock...

        }
    }
}