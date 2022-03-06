using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public bool OrderPaid { get; set; }
        public int CVV { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
