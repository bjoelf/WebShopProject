using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using WebShopProjectApp.Users;

namespace WebShopProjectApp.Database
{
    internal class DbInitializer
    {
        public static void Initialize(DBContext context, RoleManager<IdentityRole> roleManager, UserManager<User> appUser) 
        {
            context.Database.Migrate();

            if (context.Roles.Any())
                return;

            //------ Seed database ----------------------------------------
            string[] rolesToSeed = new string[] { "Admin", "User" };

            foreach (var roleName in rolesToSeed)
            {
                IdentityRole role = new IdentityRole(roleName);
                var result = roleManager.CreateAsync(role).Result;

                if (!result.Succeeded)
                    throw new Exception("Faild to create Role: " + roleName);
            }

            //Seed admin ------------------------
            User user = new User()
            {
                UserName = "AdminSeeding",
                FirstName = "Super",
                LastName = "Admin",
                StreetAdress = "Kungsgatan",
                StreetNumber = "1",
                PostalCode = "12345",
                Email = "code@elfvin.se",
                City = "Stockholm",
                PhoneNumber = "+46706952593"
            };
            IdentityResult resultUser = appUser.CreateAsync(user, "Qwerty!123").Result;
            if (!resultUser.Succeeded)
                throw new Exception("Faild to create Admin Acc: AdminSeeding");

            IdentityResult resultAssign = appUser.AddToRoleAsync(user, rolesToSeed[0]).Result;
            if (!resultAssign.Succeeded)
                throw new Exception($"Faild to grant {rolesToSeed[0]} role to AdminSeeding");
        }
    }
}
