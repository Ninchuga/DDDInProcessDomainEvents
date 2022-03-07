using Payment.Infrastructure.Repository;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System.Threading.Tasks;

namespace Payment.Application.EventHandlers
{
    public class OrderPlacedIntegrationEventHandler : IHandler<OrderPlacedIntegrationEvent>
    {
        private readonly PaymentRepository _paymentRepository;

        public OrderPlacedIntegrationEventHandler(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(OrderPlacedIntegrationEvent domainEvent)
        {
            var payment = new Domain.Entities.Payment(
                domainEvent.OrderId,
                domainEvent.CardName,
                domainEvent.CardNumber,
                domainEvent.OrderPaid,
                domainEvent.CVV,
                domainEvent.TotalPrice);

            _paymentRepository.Add(payment);
            await _paymentRepository.SaveChanges();
        }
    }
}
