using Payment.Infrastructure.Services;

namespace Payment.Domain.Services
{
    /// <summary>
    /// Example of a Domain Service
    /// Service is impure because it uses third-party resources while maintaining the domain logic
    /// </summary>
    public class PaymentService
    {
        private readonly PaymentGateway _paymentGateway;

        public PaymentService(PaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        public void Charge(Entities.Payment payment)
        {
            bool success = _paymentGateway.ChargeAmout(payment.OrderTotalPrice);
            if (!success)
                return;

            payment.Paid();
        }
    }
}
