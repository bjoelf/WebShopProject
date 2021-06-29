using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;

namespace WebShopProjectApp.ViewModels
{
    public class EditUser
    {
        public int Id { set; get; }
        public UserRegViewModel regUser { set; get; }
    }
}
