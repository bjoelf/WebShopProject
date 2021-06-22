using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.OrderItems
{
    public interface IOrderItemRepo
    {
        OrderItem Create(OrderItem orderItem);
        OrderItem ReadItem(int id);
        List<OrderItem> Read(int id);
        OrderItem Update(OrderItem orderItem);
        bool Delete(int id);
    }
}
