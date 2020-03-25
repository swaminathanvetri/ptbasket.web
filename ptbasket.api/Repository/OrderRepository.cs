using ptbasket.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ptbasket.api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        List<Order> orders = new List<Order>();
        Random random = new Random();
        public string Create(Order newOrder)
        {
            var isActiveOrderExists = orders.Exists(order => string.Compare(order.FlatNumber, newOrder.FlatNumber, true) == 0
                                        && (order.Status == OrderStatus.Created || order.Status == OrderStatus.ReadyToPickUp));
            if(!isActiveOrderExists)
            {
                newOrder.Id = string.Concat(newOrder.FlatNumber,random.Next(1, 10000));
                orders.Add(newOrder);
                return newOrder.Id;
            }
            else
            {
                throw new Exception("OrderAlreadyInProgress");
            }
        }

        public int Delete(string id)
        {
            var orderToDelete = orders.Where(order => order.Id.CompareTo(id) == 0).FirstOrDefault();
            if(orderToDelete!= null)
            {
                orders.Remove(orderToDelete);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public Order Get(string id)
        {
            return orders.Where(order => order.Id.CompareTo(id) == 0).FirstOrDefault();
        }

        public List<Order> GetActiveOrders(string userId)
        {
            if(string.Compare(userId, Constants.AdminUser,true) == 0)
            {
                return orders.Where(order => order.Status == OrderStatus.Created)
                            .OrderBy(order => order.CreatedDateTime)  
                            .ToList();
            }
            return orders.Where(order => order.Status == OrderStatus.Created && order.FlatNumber.CompareTo(userId) ==0)
                         .OrderByDescending(order => order.CreatedDateTime)        
                         .ToList();
        }

        public List<Order> GetCancelledOrders(string userId)
        {
            return orders.Where(order => order.Status == OrderStatus.Cancelled && order.FlatNumber.CompareTo(userId) == 0).ToList();
        }

        public List<Order> GetMyOrders(string userId)
        {
            if(string.Compare(userId,Constants.AdminUser,true) == 0)
            {
                return orders;
            }
            return orders.Where(order => order.FlatNumber.CompareTo(userId) == 0).ToList();
        }

        public string Update(Order orderToUpdate)
        {
            var order = orders.Where(order => order.Id.CompareTo(orderToUpdate.Id) == 0).FirstOrDefault();
            if(order!=null)
            {
                if(orderToUpdate.Status == OrderStatus.ReadyToPickUp)
                {
                    order.Status = orderToUpdate.Status;
                    order.PickUpTime = orderToUpdate.PickUpTime;
                    order.TotalAmount = orderToUpdate.TotalAmount;
                }
                if(orderToUpdate.Status == OrderStatus.Cancelled)
                {
                    if(order.Status == OrderStatus.Created)
                    {
                        order.Status = orderToUpdate.Status;
                    }
                    //else case yet to be handled to reflect the update status
                }
                if(orderToUpdate.Status == OrderStatus.PickedUp)
                {
                    order.Status = orderToUpdate.Status;
                }
                order.UpdatedTime = DateTime.Now;
            }
            
            return orderToUpdate.Id;
        }
    }
}
