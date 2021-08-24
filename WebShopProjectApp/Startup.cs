using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;

using WebShopProjectApp.Users;
using WebShopProjectApp.Products;
using WebShopProjectApp.Database;
using WebShopProjectApp.Orders;
using WebShopProjectApp.Swagger;
using Microsoft.OpenApi.Models;

namespace WebShopProjectApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //******************************** Database connection ***********************************
            //Connection to database (local). Connectionstring in appsettings.json
            services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //************* Identity ******************
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();

            //TODO: check paths Account/Login , "Account/Logout"

            //************* Cookie settings ******************
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //************* JWT settings ******************
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerOption => {
                JwtBearerOption.SaveToken = true;
                JwtBearerOption.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = Configuration["JWTConfiguration:Issuer"],
                    ValidAudience = Configuration["JWTConfiguration:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTConfiguration:SigningKey"]))
                };
            });

            //How to limit by policy on action method...
            services.AddAuthorization(config => {
                config.AddPolicy("ShouldContainRoleAdmin", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role, "Admin");
                });
            });

            //********* Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();

            //******** Repos
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            //*********** password settings
            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            //************* Cors ******************
            services.AddCors(options => {options.AddPolicy("ReactPolicy", builder => {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }); 
            });

            SwaggerModel.SwaggerSetup(services, new OpenApiInfo()
            {
                Version = "v1",
                Title = "The Sweetest Webshop API",
                Description = "WebShop CMS API V1"
            });

            //**********Swagger = API Documentatin *********
            services.AddSwaggerGen();

            //************ MVC Model..
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop CMS API V1"); });
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); 
            });
        }
    }
}
