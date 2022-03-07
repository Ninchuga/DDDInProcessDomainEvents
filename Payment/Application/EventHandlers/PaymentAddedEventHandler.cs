using Payment.Domain.Events;
using Payment.Infrastructure.Repository;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System.Threading.Tasks;

namespace Payment.Application.EventHandlers
{
    public class PaymentAddedEventHandler : IHandler<PaymentAddedEvent>
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentAddedEventHandler(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(PaymentAddedEvent domainEvent)
        {
            // Call some third party API and validate user card and check if it has funds
            // Reserve the funds if everything is ok and set Payment to Paid
            var payment = _paymentRepository.GetByOrderId(domainEvent.Payment.OrderId);
            payment.Paid();

            await _paymentRepository.SaveChanges();

            // Dispatch OrderPaid integration event to some other bounded context which is interested in that event, Order in this case
            var orderPaidEvent = new OrderPaidIntegrationEvent(domainEvent.Payment.OrderId);
            await DomainEvents.Dispatch(orderPaidEvent);
        }
    }
}
