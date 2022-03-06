using Ordering.Application.DTOs;
using Ordering.Application.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopping.UI
{
    public partial class Form1 : Form
    {
        private readonly OrderingService _orderingService;

        public Form1(OrderingService orderingService)
        {
            InitializeComponent();
            _orderingService = orderingService;
        }

        private async void placeOrderButton_Click(object sender, EventArgs e)
        {
            var orderDto = BuildMockedOrderDtoData;
            await _orderingService.PlaceOrder(orderDto);
        }

        private OrderDto BuildMockedOrderDtoData =>
            new OrderDto
            {
                OrderId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    UserName = "luckylou",
                    UserEmail = "useremail@gmail.com",
                    TotalPrice = 550,
                    OrderStatus = "Pending",
                    OrderDate = DateTime.UtcNow,
                    CardName = "Lucky Lou",
                    CardNumber = "12345677890",
                    OrderPaid = false,
                    CVV = 333,
                    OrderItems = new List<OrderItemDto>
                    {
                        new OrderItemDto{ ProductId = "4334", Discount = 20, Price = 120, ProductName = "Coffe Machine", Quantity = 1 }
                    }
            };
    }
}
