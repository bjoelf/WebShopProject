using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.Users
{
    public interface IUserRepo
    {
        User Create(User user);
        User Read(int id);
        List<User> Read();
        User Edit(User user);
        bool Delete(int id);
    }
}
