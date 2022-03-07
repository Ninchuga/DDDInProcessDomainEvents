using Payment.Domain.Events;
using SharedKernel;
using System;

namespace Payment.Domain.Entities
{
    public class Payment : AggregateRoot<int>
    {
        public Payment(Guid orderId, string cardName, string cardNumber, bool orderPaid, int cVV, decimal orderTotalPrice)
            : base(default)
        {
            OrderId = orderId;
            CardName = cardName;
            CardNumber = cardNumber;
            OrderPaid = orderPaid;
            CVV = cVV;
            OrderTotalPrice = orderTotalPrice;

            AddDomainEvent(new PaymentAddedEvent(this));
        }

        public Guid OrderId { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public bool OrderPaid { get; private set; }
        public int CVV { get; private set; }
        public decimal OrderTotalPrice { get; private set; }

        public void Paid() => OrderPaid = true;
    }
}
