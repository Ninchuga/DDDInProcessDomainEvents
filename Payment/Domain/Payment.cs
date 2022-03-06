using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
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
        }

        public Guid OrderId { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public bool OrderPaid { get; private set; }
        public int CVV { get; private set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
