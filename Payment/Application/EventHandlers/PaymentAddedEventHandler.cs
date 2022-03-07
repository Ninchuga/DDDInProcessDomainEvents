using Payment.Domain.Events;
using Payment.Domain.Services;
using Payment.Infrastructure.Repository;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System.Threading.Tasks;

namespace Payment.Application.EventHandlers
{
    public class PaymentAddedEventHandler : IHandler<PaymentAddedEvent>
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly PaymentService _paymentService;

        public PaymentAddedEventHandler(PaymentRepository paymentRepository, PaymentService paymentService)
        {
            _paymentRepository = paymentRepository;
            _paymentService = paymentService;
        }

        public async Task Handle(PaymentAddedEvent domainEvent)
        {
            var payment = _paymentRepository.GetByOrderId(domainEvent.Payment.OrderId);

            _paymentService.Charge(payment);

            if(payment.OrderPaid)
            {
                await _paymentRepository.SaveChanges();

                // Dispatch OrderPaid integration event to some other bounded context which is interested in that event, Order in this case
                var orderPaidEvent = new OrderPaidIntegrationEvent(domainEvent.Payment.OrderId);
                await DomainEvents.Dispatch(orderPaidEvent);
            }
        }
    }
}
