using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Users
{
    public interface IUserService
    {
        Task<User> Add(RegisterUser userReg);
        List<User> All();
        User FindById(string id);
        User Edit(string id, RegisterUser userReg);
    }
}
