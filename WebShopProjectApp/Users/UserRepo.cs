using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Database;

namespace WebShopProjectApp.Users
{
    public class UserRepo : IUserRepo
    {
        private readonly DBContext _dBContext;
        public UserRepo(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public User Create(User user)
        {
            _dBContext.Add(user);
            int ret = _dBContext.SaveChanges();
            if (ret == 0)
                return null;

            return user;
        }
        public List<User> Read()
        {
            if (_dBContext.Users.Count() > 0)
                return _dBContext.Users.ToList();
            return null;
        }
        public User Read(string id)
        {
            return _dBContext.Users.Find(id);
        }
        public User Update(User user)
        {
            User u = Read(user.Id);

            if (u == null)
                return null;

            _dBContext.Update(u);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return null;

            return u;
        }
        public bool Delete(string id)
        {
            User u = Read(id);

            if (u == null)
                return false;
            _dBContext.Users.Remove(u);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return false;

            return true;
        }
    }
}
