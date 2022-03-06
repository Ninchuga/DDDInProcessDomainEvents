using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.IntegrationEvents
{
    public class OrderPlacedIntegrationEvent : IDomainEvent
    {
        public OrderPlacedIntegrationEvent(Guid orderId, decimal totalPrice, DateTime orderDate, string cardName, string cardNumber, bool orderPaid, int cVV)
        {
            OrderId = orderId;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            CardName = cardName;
            CardNumber = cardNumber;
            OrderPaid = orderPaid;
            CVV = cVV;
        }

        public Guid OrderId { get; }
        public decimal TotalPrice { get; }
        public DateTime OrderDate { get; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public bool OrderPaid { get; private set; }
        public int CVV { get; private set; }
    }
}
