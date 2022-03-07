using Ordering.Domain.Entitites;
using Ordering.Infrastructure.Repositories;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System.Threading.Tasks;

namespace Ordering.Application.EventHandlers
{
    public class OrderPaidEventHandler : IHandler<OrderPaidIntegrationEvent>
    {
        private readonly OrderRepository _orderRepository;

        public OrderPaidEventHandler(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderPaidIntegrationEvent domainEvent)
        {
            Order order = _orderRepository.GetById(domainEvent.OrderId);
            order.Paid();

            await _orderRepository.SaveChanges();
        }
    }
}
