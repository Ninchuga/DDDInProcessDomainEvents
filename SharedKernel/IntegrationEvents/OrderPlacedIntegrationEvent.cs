using System;

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
        public string CardName { get; set; }
        public string CardNumber { get; }
        public bool OrderPaid { get; }
        public int CVV { get; }
    }
}
