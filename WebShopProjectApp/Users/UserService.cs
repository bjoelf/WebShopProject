using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepo userRepo, RoleManager<IdentityRole> roleManager, UserManager<User> User)
        {
            _userRepo = userRepo;
            _roleManager = roleManager;
            _userManager = User;
        }
        public User Add(UserRegViewModel userReg)
        {
            User u = new User() {
                UserName = userReg.UserName,
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                StreetAdress = userReg.StreetAdress,
                StreetNumber = userReg.StreetNumber,
                PostalCode = userReg.PostalCode,
                City = userReg.City,
                Email = userReg.Email,
                Phone = userReg.Phone,
            };

            try
            {
                IdentityResult resultUser = _userManager.CreateAsync(u, userReg.Password).Result;
                resultUser = _userManager.AddToRoleAsync(u, "User").Result;
            } 
            catch 
            {
                throw new Exception($"Faild to create user:  {u.FirstName}  {u.LastName}");
            }

            return _userRepo.Create(u);
        }
        public List<User> All()
        {
            return _userRepo.Read();
        }
        public User Edit(int id, UserRegViewModel userReg)
        {
            User u = FindById(id);
            if (u == null)
                return null;

            u.UserName = userReg.UserName;
            u.FirstName = userReg.FirstName;
            u.LastName = userReg.LastName;
            u.StreetAdress = userReg.StreetAdress;
            u.StreetNumber = userReg.StreetNumber;
            u.PostalCode = userReg.PostalCode;
            u.City = userReg.City;
            u.Email = userReg.Email;
            u.Phone = userReg.Phone;

            u = _userRepo.Update(u);

            return u;
        }
        public User FindById(int id)
        {
            return _userRepo.Read(id);
        }
    }
}
