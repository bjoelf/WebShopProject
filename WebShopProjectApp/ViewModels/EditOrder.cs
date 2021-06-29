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
        public CreateOrder CreateOrder { set; get; }
        public User Customer { set; get; }
    }
}
