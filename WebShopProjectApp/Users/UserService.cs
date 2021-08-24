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
        public async Task<User> Add(RegisterUser userReg)
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
                IdentityResult resultUser = await _userManager.CreateAsync(u, userReg.Password);

                //TODO:  Kolla success innan roll tilldelning!


                resultUser = _userManager.AddToRoleAsync(u, "User").Result;
                //det blir inten exeption här...
                //Kolla resultUser status innan gå vidare....
            } 
            catch 
            {
                throw new Exception($"Faild to create user:  {u.FirstName}  {u.LastName}");
            }

            //TODO: ändra retur data format.
            //Returnera username = email??
            return _userRepo.Create(u);


        }
        public List<User> All()
        {
            return _userRepo.Read();
        }
        public User Edit(string id, RegisterUser userReg)
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
        public User FindById(string id)
        {
            return _userRepo.Read(id);
        }
    }
}
