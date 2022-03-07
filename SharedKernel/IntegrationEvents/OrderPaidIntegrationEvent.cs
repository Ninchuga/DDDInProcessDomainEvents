using System;

namespace SharedKernel.IntegrationEvents
{
    public class OrderPaidIntegrationEvent : IDomainEvent
    {
        public OrderPaidIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
