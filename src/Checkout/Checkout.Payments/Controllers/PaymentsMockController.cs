using System;
using System.Linq;
using Checkout.Payments.Contracts;
using Checkout.Payments.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payments.Controllers
{
    [Route("api/payments/mock/")]
    [ApiController]
    public class PaymentsMockController : ControllerBase
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public PaymentsMockController(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
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
        public ActionResult<string> Get(Guid providerReference, bool success)
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
                paymentMock.PaymentStatus = PaymentStatus.Completed;
            }
            else
            {
                paymentMock.PaymentStatus = PaymentStatus.Failed;
            }
            // todo send events
            _paymentsDbContext.SaveChanges();

            return Ok(new { returnUrl = "/orderPlaced" }); // todo thats mock of mock...

        }
    }
}