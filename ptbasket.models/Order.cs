using System;
using System.Collections.Generic;

namespace ptbasket.models
{
    public class Order
    {
        public string Id { get; set; }
        public string FlatNumber { get; set; }
        public string MobileNumber { get; set; }
        public List<Item> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public Double TotalAmount { get; set; }
    }

    public enum OrderStatus
    {
        Created,
        ReadyToPickUp,
        PickedUp,
        Cancelled
    }

    public class Item
    {
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Quantity { get; set; }
    }
}
