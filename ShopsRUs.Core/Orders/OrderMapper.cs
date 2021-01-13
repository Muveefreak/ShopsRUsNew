using ShopsRUs.Core.Orders.Commands;
using ShopsRUs.Core.Orders.Responses;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Orders
{
    public static class OrderMapper
    {
        public static Order ToCreateEntity(this CreateOrderCommand command)
        {
            var result = new Order
            {
                CustomerId = command.CustomerId,
                ItemName = command.ItemName,
                OrderType = command.OrderType,
                OrderStatus = command.OrderStatus,
                Amount = command.Amount,
            };

            return result;
        }

        public static OrderResponse ToResponse(this Order order)
        {
            var result = new OrderResponse
            {
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                ItemName = order.ItemName,
                OrderId = order.OrderId,
                OrderType = order.OrderType,
            };

            return result;
        }
    }
}
