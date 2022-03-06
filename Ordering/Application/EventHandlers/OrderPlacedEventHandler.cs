using Ordering.Domain.Events;
using Ordering.Infrastructure.Mail;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.EventHandlers
{
    public class OrderPlacedEventHandler : IHandler<OrderPlacedEvent>
    {
        private readonly EmailService _emailService;

        public OrderPlacedEventHandler()
        {
            _emailService = new EmailService();
        }

        public void Handle(OrderPlacedEvent domainEvent)
        {
            _emailService.SendOrderPlacedEmail(domainEvent.OrderId, domainEvent.TotalPrice, domainEvent.UserEmail);

            var orderPlacedIntegrationEvent = new OrderPlacedIntegrationEvent(
                domainEvent.OrderId,
                domainEvent.TotalPrice,
                domainEvent.OrderDate,
                domainEvent.PaymentData.CardName,
                domainEvent.PaymentData.CardNumber,
                domainEvent.PaymentData.OrderPaid,
                domainEvent.PaymentData.CVV);

            DomainEvents.Dispatch(orderPlacedIntegrationEvent);
        }
    }
}
