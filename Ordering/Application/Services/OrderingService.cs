using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Domain.Entitites;
using Ordering.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public class OrderingService
    {
        private readonly OrderRepository _orderRepository;

        public OrderingService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task PlaceOrder(OrderDto orderDto)
        {
            Order order = orderDto.ToEntity();
            foreach (var item in orderDto.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.Discount, item.Quantity);
            }

            await _orderRepository.Add(order);

            // we can create in here OrderPlacedIntegrationEvent and dispatch it to Payment context after Order is successfully commited
            // var orderPlacedIntegrationEvent = new OrderPlacedIntegrationEvent ...
            //DomainEvents.Dispatch(orderPlacedIntegrationEvent);
        }
    }
}
