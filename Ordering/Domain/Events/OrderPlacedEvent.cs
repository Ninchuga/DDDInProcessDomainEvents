using Ordering.Domain.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Events
{
    public class OrderPlacedEvent : IDomainEvent
    {
        public OrderPlacedEvent(Guid orderId, decimal totalPrice, DateTime orderDate, string userEmail, PaymentData paymentData)
        {
            OrderId = orderId;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            UserEmail = userEmail;
            PaymentData = paymentData;
        }

        public Guid OrderId { get; }
        public decimal TotalPrice { get; }
        public DateTime OrderDate { get; }
        public string UserEmail { get; }
        public PaymentData PaymentData { get; }
    }
}
