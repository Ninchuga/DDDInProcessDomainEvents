using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entitites;
using Ordering.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public Order GetById(Guid orderId)
        {
            var order = _orderContext.Orders
                .Include(order => order.OrderItems)
                .FirstOrDefault(o => o.Id.Equals(orderId));

            return order;
        }

        public async Task Add(Order order)
        {
            _orderContext.Orders.Add(order);

            await _orderContext.SaveChanges(order);
        }

        //private bool SuccessfullySavedBy(int insertedEntitiesNumber) => insertedEntitiesNumber > 0;
    }
}
