using Ordering.Domain.Exceptions;
using SharedKernel;
using System.Collections.Generic;

namespace Ordering.Domain.ValueObjects
{
    public class PaymentData : ValueObject
    {
        private PaymentData() { }

        private PaymentData(string cardName, string cardNumber, bool orderPaid, int cvv)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            OrderPaid = orderPaid;
            CVV = cvv;
        }

        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public bool OrderPaid { get; private set; }
        public int CVV { get; private set; }

        public static PaymentData From(string cardName, string cardNumber, bool orderPaid, int cvv)
        {
            if (string.IsNullOrWhiteSpace(cardName))
                throw new PaymentDataException("Card name is mandatory");
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new PaymentDataException("Card number is mandatory");

            return new PaymentData(cardName, cardNumber, orderPaid, cvv);
        }

        public PaymentData PaidOrder() => From(CardName, CardNumber, true, CVV);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CardName;
            yield return CardNumber;
            yield return OrderPaid;
            yield return CVV;
        }
    }
}
