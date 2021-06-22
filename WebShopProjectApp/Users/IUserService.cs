using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Users
{
    public interface IUserService
    {
        User Add(CreateUser createUser);
        List<User> All();
        User FindById(int id);
        User Edit(int id, CreateUser createUser);
    }
}
