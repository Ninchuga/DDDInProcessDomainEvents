using Payment.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Repository
{
    public class PaymentRepository
    {
        private readonly PaymentContext _paymentContext;

        public PaymentRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public Domain.Entities.Payment GetById(Guid paymentId)
        {
            var payment = _paymentContext.Payments
                .FirstOrDefault(o => o.Id.Equals(paymentId));

            return payment;
        }

        public Domain.Entities.Payment GetByOrderId(Guid orderId)
        {
            var payment = _paymentContext.Payments
                .FirstOrDefault(o => o.OrderId.Equals(orderId));

            return payment;
        }

        public void Add(Domain.Entities.Payment payment)
        {
            _paymentContext.Payments.Add(payment);
        }

        public async Task SaveChanges() =>
            await _paymentContext.SaveChanges();
    }
}
