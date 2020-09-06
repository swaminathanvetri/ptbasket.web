using ptbasket.api.Repository.Models;
using ptbasket.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ptbasket.api
{
    public class Utils
    {
        public static OrderEntity CreateOrderEntity(Order order)
        {
            OrderEntity orderEntity = new OrderEntity(order.FlatNumber, order.MobileNumber);
            orderEntity.OrderId = order.Id;
            orderEntity.Status = order.Status.ToString();
            orderEntity.TotalAmount = order.TotalAmount;
            orderEntity.CreatedTimestamp = order.CreatedDateTime;
            orderEntity.UpdatedTimestamp = order.UpdatedTime;
            orderEntity.PickUpTimeStamp = order.PickUpTime;
            orderEntity.Items = JsonSerializer.Serialize<List<Item>>(order.Items);

            return orderEntity;
        }
    }
}
