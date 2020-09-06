using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ptbasket.api
{
    public static class Constants
    {
        public static string OrderAlreadyInProgress = "OrderAlreadyInProgress";
        public static string AdminUser = "admin";
        public static string UserIdKey = "x-user-id";

        public static string PtOrders { get; internal set; }
    }
}
