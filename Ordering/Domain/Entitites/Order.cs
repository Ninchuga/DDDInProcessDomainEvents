using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Entitites
{
    public class Order : AggregateRoot<Guid>
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string UserEmail { get; }
        public decimal TotalPrice { get; }
        public string OrderStatus { get; private set; }
        public DateTime OrderDate { get; }
        public DateTime? OrderCancellationDate { get; private set; }

        public PaymentData PaymentData { get; private set; }

        private List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Guid orderId, Guid userId, string userName, string userEmail, decimal totalPrice, string orderStatus, DateTime orderDate, PaymentData paymentData)
            : this(orderId, userId, userName, userEmail, totalPrice, orderStatus, orderDate)
        {
            PaymentData = paymentData;
            //AddDomainEvent(new OrderPlacedEvent(orderId, totalPrice, orderDate, userEmail, PaymentData));
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Order(Guid id, Guid userId, string userName, string userEmail, decimal totalPrice, string orderStatus, DateTime orderDate)
            : base(id)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            TotalPrice = totalPrice;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productId, string productName, decimal itemPrice, decimal discount, int quantity = 1)
        {
            var existingOrderForProduct = OrderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.UpdateQuantity(quantity);
            }
            else
            {
                var orderItem = new OrderItem(productId, quantity, itemPrice, productName, discount);
                _orderItems.Add(orderItem);
            }
        }
    }
}
