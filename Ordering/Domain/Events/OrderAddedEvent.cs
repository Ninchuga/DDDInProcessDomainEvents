using Ordering.Domain.Entitites;
using SharedKernel;

namespace Ordering.Domain.Events
{
    public class OrderAddedEvent : IDomainEvent
    {
        public OrderAddedEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
