using System;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Payments.Contracts;
using Checkout.Payments.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payments.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public PaymentsController(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
        }

        [AllowAnonymous]
        [HttpGet("methods")]
        public async Task<IActionResult> GetMethods()
        {
            return Ok(PaymentMethods.GetAll);
        }

        [AllowAnonymous]
        [HttpGet("{paymentReference}")]
        public ActionResult<string> Get(Guid paymentReference)
        {
            // todo only mine?
            var payment = _paymentsDbContext.Payments.FirstOrDefault(x => x.PaymentReference == paymentReference);
            if (payment == null)
                return NoContent();

            if (payment.PaymentStatus != PaymentStatus.New)
            {
                return BadRequest(); // todo some logic..
            }

            if (payment.PaymentMethod == PaymentMethods.PaymentProvider)
            {
                var paymentMock = _paymentsDbContext.MockPayments.FirstOrDefault(x => x.PaymentReference == payment.PaymentReference);
                if (paymentMock == null)
                {
                    return Accepted();
                }

                return Ok(new { paymentUrl = $"/payment-mock/{paymentMock.ProviderReference}" }); // todo model?
            }

            return BadRequest();
        }
        
    }
}
