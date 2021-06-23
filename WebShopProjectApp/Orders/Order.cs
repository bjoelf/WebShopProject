using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Users;

namespace WebShopProjectApp.Orders
{
    public class Order
    {
        List<OrderItem> items { get; set; }
        User Delivery { get; set; }
    }
}
