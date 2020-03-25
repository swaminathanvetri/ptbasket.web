using System;
using System.Collections.Generic;

namespace ptbasket.models
{
    public class PickUpSlot
    {
        public int Id { get; set; }
        public DateTime PickUpTime { get; set; }
        public List<Order> Orders { get; set; }
    }
}
