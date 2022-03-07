using Ordering.Domain.Events;
using Ordering.Infrastructure.Mail;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System.Threading.Tasks;

namespace Ordering.Application.EventHandlers
{
    public class OrderAddedEventHandler : IHandler<OrderAddedEvent>
    {
        private readonly EmailService _emailService;

        public OrderAddedEventHandler(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(OrderAddedEvent domainEvent)
        {
            _emailService.SendOrderPlacedEmail(domainEvent.Order.Id, domainEvent.Order.TotalPrice, domainEvent.Order.UserEmail);

            var orderPlacedIntegrationEvent = new OrderPlacedIntegrationEvent(
                domainEvent.Order.Id,
                domainEvent.Order.TotalPrice,
                domainEvent.Order.OrderDate,
                domainEvent.Order.PaymentData.CardName,
                domainEvent.Order.PaymentData.CardNumber,
                domainEvent.Order.PaymentData.OrderPaid,
                domainEvent.Order.PaymentData.CVV);

            await DomainEvents.Dispatch(orderPlacedIntegrationEvent);
        }
    }
}
