using ptbasket.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ptbasket.api.Repository
{
    public interface IOrderRepository
    {
        string Create(Order newOrder);
        int Delete(string id);
        Order Get(string id);
        List<Order> GetActiveOrders(string userId);
        List<Order> GetMyOrders(string userId);
        List<Order> GetCancelledOrders(string userId);
        string Update(Order order);
    }
}
