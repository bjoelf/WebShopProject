using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.Users
{
    public interface IUserRepo
    {
        User Create(User user);
        User Read(string id);
        List<User> Read();
        User Update(User user);
        bool Delete(string id);
    }
}
