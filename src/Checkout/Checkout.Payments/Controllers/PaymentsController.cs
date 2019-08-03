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
        public async Task<IActionResult> GetMethods(string deliveryMethod)
        {
            var availableMethods = PaymentMethod.GetAll
                .Where(x => x.SupportedDeliveryMethods.Any(sdm =>
                    sdm.DeliveryMethodName.Equals(deliveryMethod, StringComparison.InvariantCultureIgnoreCase)))
                .Select(x =>
                {
                    var price = x.SupportedDeliveryMethods.FirstOrDefault(sdm =>
                        sdm.DeliveryMethodName.Equals(deliveryMethod, StringComparison.InvariantCultureIgnoreCase));
                    return new PaymentMethodDto()
                    {
                        Name = x.Name,
                        Fee = price.PaymentFee
                    };
                });

            return Ok(availableMethods);
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

            if (payment.PaymentMethod == PaymentMethod.PaymentProvider.Name)
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
