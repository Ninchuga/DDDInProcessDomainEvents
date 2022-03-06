using Ordering.Application.DTOs;
using Ordering.Domain.Entitites;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static Order ToEntity(this OrderDto orderDto)
        {
            return new Order(
                orderDto.OrderId,
                orderDto.UserId,
                orderDto.UserName,
                orderDto.UserEmail,
                orderDto.TotalPrice,
                orderDto.OrderStatus,
                orderDto.OrderDate,
                PaymentData.From(orderDto.CardName, orderDto.CardNumber, orderDto.OrderPaid, orderDto.CVV));
        }
    }
}
