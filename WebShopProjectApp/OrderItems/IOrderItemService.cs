using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.OrderItems
{
    interface IOrderItemService
    {
        OrderItem Add(CreateOrderItem orderItem);
    }
}
