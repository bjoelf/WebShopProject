using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Users;

namespace WebShopProjectApp.ViewModels
{
    public class EditOrder
    {
        public int Id { set; get; }
        public CreateOrder createOrder { set; get; }
        public EditOrder(int id, CreateOrder createOrder)
        {
            this.createOrder = createOrder;
            Id = id;
        }
    }
}
