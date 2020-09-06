using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ptbasket.api.Repository.Models
{
    public class OrderEntity : TableEntity
    {
        public OrderEntity()
        {

        }

        public OrderEntity(string flatNumber, string mobileNumber)
        {
            PartitionKey = flatNumber;
            RowKey = mobileNumber;
        }

        public string OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public DateTime PickUpTimeStamp { get; set; }
        public Double TotalAmount { get; set; }
        public string Items { get; set; }

    }
}
