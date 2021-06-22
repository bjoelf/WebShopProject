using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebShopProjectApp.Users;
using WebShopProjectApp.Products;
using WebShopProjectApp.Orders;

namespace WebShopProjectApp.Database
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

// Checklist for entity fast steps
// Done: Nuget: Microsoft.EntityFrameworkCore.SqlServer + Microsoft.EntityFrameworkCore.Design
// Done: Create your DbContext class file.
// Done: Create/Edit your appsettings.json file to contain
// Done: Add access to appsettings.json in your Startup class file
// Done: Add your DbContext to ConfigureServices in Startup class file.
// Done: Connect your IExRepo to ExRepo in ConfigureServices in Startup class file
// Done: Dependency Inject your ExDbContext into your ExRepo class file

//Kommandon i PM:
// REBUILD
// dotnet ef migrations add COMMENT
// REBUILD
// dotnet ef database update
// DONE!