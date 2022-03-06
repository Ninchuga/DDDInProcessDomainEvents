using Payment.Infrastructure.Persistence;
using Payment.Infrastructure.Repository;
using SharedKernel;
using SharedKernel.IntegrationEvents;

namespace Payment.Application.EventHandlers
{
    public class OrderPlacedIntegrationEventHandler : IHandler<OrderPlacedIntegrationEvent>
    {
        private readonly PaymentRepository _paymentRepository;

        public OrderPlacedIntegrationEventHandler()
        {
            _paymentRepository = new PaymentRepository(new PaymentContext());
        }

        public void Handle(OrderPlacedIntegrationEvent domainEvent)
        {
            var payment = new Domain.Payment(
                domainEvent.OrderId,
                domainEvent.CardName,
                domainEvent.CardNumber,
                domainEvent.OrderPaid,
                domainEvent.CVV,
                domainEvent.TotalPrice);

            _paymentRepository.Add(payment).GetAwaiter().GetResult();

            // If everything is ok and Payment is added to db
            // Dispatch OrderPaid integration event to some other bounded context which is interested in that event

        }
    }
}
