using AutoMapper;
using Ordering.Application.DTOs;
using Ordering.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(destination => destination.OrderId, op => op.MapFrom(source => source.Id))
                .ForMember(destination => destination.CardName, op => op.MapFrom(source => source.PaymentData.CardName))
                .ForMember(destination => destination.CardNumber, op => op.MapFrom(source => source.PaymentData.CardNumber))
                .ForMember(destination => destination.OrderPaid, op => op.MapFrom(source => source.PaymentData.OrderPaid))
                .ForMember(destination => destination.CVV, op => op.MapFrom(source => source.PaymentData.CVV))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
