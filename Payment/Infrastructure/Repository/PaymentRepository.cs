using Payment.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Repository
{
    public class PaymentRepository
    {
        private readonly PaymentContext _orderContext;

        public PaymentRepository(PaymentContext orderContext)
        {
            _orderContext = orderContext;
        }

        public Domain.Payment GetById(Guid paymentId)
        {
            var order = _orderContext.Payments
                .FirstOrDefault(o => o.Id.Equals(paymentId));

            return order;
        }

        public async Task Add(Domain.Payment payment)
        {
            _orderContext.Payments.Add(payment);

            await _orderContext.SaveChanges(payment).ConfigureAwait(false);
        }
    }
}
